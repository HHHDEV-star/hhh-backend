using Amazon.S3;
using hhh.infrastructure.Auth;
using hhh.infrastructure.Context;
using hhh.infrastructure.Logging;
using hhh.infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hhh.infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// 註冊 Infrastructure 層服務（DbContext、JWT token 產生器等技術實作）
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 共用的 MySQL 版本（所有 DbContext 都走同一顆 server）
        var mysqlVersionRaw = configuration["MySql:Version"] ?? "8.0.45-mysql";
        var serverVersion = ServerVersion.Parse(mysqlVersionRaw);

        // Xoops (legacy) DbContext — 連線字串從 appsettings 讀取
        var xoopsConn = configuration.GetConnectionString("Xoops")
            ?? throw new InvalidOperationException("ConnectionStrings:Xoops is not configured.");

        services.AddDbContext<XoopsContext>(options =>
            options.UseMySql(xoopsConn, serverVersion, mysql => mysql.UseNetTopologySuite()));

        // hhh_backstage DbContext（ACL / backend 相關）
        var backstageConn = configuration.GetConnectionString("HHHBackstage")
            ?? throw new InvalidOperationException("ConnectionStrings:HHHBackstage is not configured.");

        services.AddDbContext<HHHBackstageContext>(options =>
            options.UseMySql(backstageConn, serverVersion));

        // hhh_api DbContext（REST log 相關）
        var hhhApiConn = configuration.GetConnectionString("HhhApi")
            ?? throw new InvalidOperationException("ConnectionStrings:HhhApi is not configured.");

        services.AddDbContext<HhhApiContext>(options =>
            options.UseMySql(hhhApiConn, serverVersion));

        // JWT token 產生器（純技術實作，跨 application 層共用）
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // 檔案上傳
        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.SectionName));

        var provider = configuration[$"{StorageOptions.SectionName}:Provider"] ?? "S3";
        if (string.Equals(provider, "S3", StringComparison.OrdinalIgnoreCase))
        {
            // AWS S3 實作
            services.Configure<S3Options>(configuration.GetSection(S3Options.SectionName));
            services.AddSingleton<IAmazonS3>(_ =>
            {
                var region = configuration["AWS:Region"] ?? "ap-northeast-1";
                return new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(region));
            });
            services.AddSingleton<IImageUploadService, S3ImageUploadService>();
        }
        else
        {
            // 本機磁碟實作（開發環境 fallback）
            services.AddSingleton<IImageUploadService, LocalImageUploadService>();
        }

        // 操作紀錄寫入（對應舊 PHP 的 _save_log()）
        // 注意：IOperationContextAccessor 由 Web host 層（hhh.webapi.admin）註冊，
        //       infrastructure 不直接依賴 Microsoft.AspNetCore.Http。
        services.AddScoped<IOperationLogWriter, OperationLogWriter>();

        return services;
    }
}
