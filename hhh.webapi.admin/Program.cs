using System.Text;
using System.Text.Json;
using hhh.api.contracts.Common;
using hhh.application.admin;
using hhh.infrastructure;
using hhh.infrastructure.Logging;
using hhh.infrastructure.Storage;
using hhh.webapi.admin.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Controllers + 統一的 400 模型驗證錯誤回應
// ---------------------------------------------------------------------------
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .SelectMany(e => e.Value!.Errors.Select(err => err.ErrorMessage))
                .ToList();

            var message = errors.Count > 0 ? string.Join("; ", errors) : "參數錯誤";
            return new BadRequestObjectResult(ApiResponse.Error(400, message));
        };
    });

// ---------------------------------------------------------------------------
// Swagger with Bearer auth
// ---------------------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HHH Admin Web API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "輸入 JWT Token，格式: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

// ---------------------------------------------------------------------------
// Layered DI
// ---------------------------------------------------------------------------
builder.Services.AddInfrastructure(builder.Configuration);  // DbContext + JWT token generator + IOperationLogWriter
builder.Services.AddAdminApplication();                     // AuthService, UserService

// HTTP 上下文 + 操作紀錄 context accessor（infrastructure 只定義抽象，Web host 在這裡綁定實作）
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOperationContextAccessor, HttpOperationContextAccessor>();

// ---------------------------------------------------------------------------
// JWT Authentication (presentation concern — stays in Web host)
// ---------------------------------------------------------------------------
var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["SecretKey"]
    ?? throw new InvalidOperationException("Jwt:SecretKey is not configured.");

// 共用的 JSON 序列化選項（與 MVC 預設 camelCase 一致）
var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        // 統一 401 / 403 回應為 ApiResponse，避免預設空 body
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                // 預設行為會寫 WWW-Authenticate header 並回 401 空 body，
                // HandleResponse() 阻止後我們自己寫 JSON。
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json; charset=utf-8";

                var message = string.IsNullOrEmpty(context.ErrorDescription)
                    ? "未授權或 token 無效"
                    : context.ErrorDescription;

                var body = ApiResponse.Error(401, message);
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(body, jsonOptions));
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json; charset=utf-8";

                var body = ApiResponse.Error(403, "權限不足");
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(body, jsonOptions));
            }
        };
    });

builder.Services.AddAuthorization();

// ---------------------------------------------------------------------------
// CORS（前後端分離）
// ---------------------------------------------------------------------------
const string CorsPolicy = "DefaultCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ---------------------------------------------------------------------------
// Global exception handler — 任何未捕捉的例外都轉成 ApiResponse，
// 放在 pipeline 最前面，確保其他 middleware 拋出的例外也會被接住。
// ---------------------------------------------------------------------------
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = feature?.Error;

        // 伺服端仍然要留紀錄（開發環境會顯示細節，正式環境只回訊息）
        if (ex is not null)
        {
            var logger = context.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("GlobalExceptionHandler");
            logger.LogError(ex, "Unhandled exception on {Path}", context.Request.Path);
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json; charset=utf-8";

        var message = app.Environment.IsDevelopment() && ex is not null
            ? ex.Message
            : "伺服器內部錯誤";

        var body = ApiResponse.Error(500, message);
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(body, jsonOptions));
    });
});

// ---------------------------------------------------------------------------
// 未匹配的路由 / 405 / 其他非成功狀態碼統一包成 ApiResponse
// （只在 response body 為空時才接管）
// ---------------------------------------------------------------------------
app.UseStatusCodePages(async statusContext =>
{
    var response = statusContext.HttpContext.Response;
    if (response.HasStarted) return;

    response.ContentType = "application/json; charset=utf-8";

    var message = response.StatusCode switch
    {
        StatusCodes.Status404NotFound => "找不到對應的 API",
        StatusCodes.Status405MethodNotAllowed => "HTTP 方法不被允許",
        _ => $"請求錯誤 ({response.StatusCode})"
    };

    var body = ApiResponse.Error(response.StatusCode, message);
    await response.WriteAsync(
        JsonSerializer.Serialize(body, jsonOptions));
});

// ---------------------------------------------------------------------------
// HTTP pipeline
// ---------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---------------------------------------------------------------------------
// 靜態檔案：把 Storage:LocalUploadRoot 掛到 Storage:PublicUrlPrefix
// 讓 /uploads/xxx 可以直接存取已上傳的圖檔
// ---------------------------------------------------------------------------
{
    var storageOptions = app.Services.GetRequiredService<IOptions<StorageOptions>>().Value;
    var uploadRoot = Path.IsPathRooted(storageOptions.LocalUploadRoot)
        ? Path.GetFullPath(storageOptions.LocalUploadRoot)
        : Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, storageOptions.LocalUploadRoot));
    Directory.CreateDirectory(uploadRoot);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(uploadRoot),
        RequestPath = storageOptions.PublicUrlPrefix.TrimEnd('/'),
        ServeUnknownFileTypes = false,
    });
}

app.UseCors(CorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
