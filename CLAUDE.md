# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

.NET 8 Web API that is being migrated from a legacy PHP/Xoops CMS. The backing MySQL schema is **not** changing — EF Core entities were scaffolded from the existing `xoops` database (~150 tables) and business logic is being rewritten endpoint-by-endpoint from the PHP `/backend/action/*.php` scripts.

Source comments, API messages and commit messages are in **Traditional Chinese**. When editing code, preserve the existing Chinese XML doc comments.

## Common commands

Run everything from the solution root (`D:\github.com\HHHDEV-star\hhh-backend`).

```powershell
# Build
dotnet build hhh-backend.sln

# Run the admin Web API (Swagger UI at http://localhost:5077/swagger)
dotnet run --project hhh.webapi.admin

# Clean (use when switching branches or after rename operations)
dotnet clean hhh-backend.sln

# Add / remove a project
dotnet sln add  <project>/<project>.csproj
dotnet sln remove <project>/<project>.csproj

# Restore a single project
dotnet restore hhh.webapi.admin/hhh.webapi.admin.csproj
```

There is no test project yet. If you add one, wire it into `hhh-backend.sln` and use `dotnet test`.

## Solution layout (Clean Architecture)

```
hhh-backend.sln
├── hhh.api.contracts          (net8.0, zero deps) — shared API request/response DTOs (e.g. ApiResponse<T>)
├── hhh.api.contracts.admin    (net8.0, zero deps) — admin-specific request/response DTOs
├── hhh.infrastructure         (net8.0)           — EF Core DbContext, ~150 scaffolded entities, JWT token generator, other technical implementations
├── hhh.application.admin      (net8.0)           — admin business logic (use cases), no ASP.NET Core dependency
└── hhh.webapi.admin           (net8.0 Web API)   — HTTP host: Controllers, Program.cs, Swagger, JWT validation, CORS
```

Dependency direction is strictly one-way:

```
hhh.webapi.admin ──► hhh.application.admin ──► hhh.infrastructure
        │                      │                       │
        └──────────────────────┴──► hhh.api.contracts(.admin)
```

### Rules for each layer

- **`hhh.api.contracts*`** — pure DTOs, no behaviour, no framework references. Anything on the wire between client and server lives here. Split: shared types in `hhh.api.contracts`, admin-only types in `hhh.api.contracts.admin`. Do not let these projects reference anything else.
- **`hhh.infrastructure`** — owns `XoopsContext` and all ~150 scaffolded entities in `Dto/Xoops/` (namespace `hhh.infrastructure.Dto.Xoops`), plus non-business technical implementations like `Auth/JwtTokenGenerator`. May reference `Microsoft.Extensions.*` abstractions. Do not put business rules here. Additional databases will each get their own subfolder (`Dto/<DbName>/`) and their own `DbContext` in `Context/`.
- **`hhh.application.admin`** — use cases written as feature folders (`Auth/`, `Users/`, …). Each feature owns its service interface, implementation, and any **domain `*Result` type** (see `LoginResult`) that represents success/failure without HTTP concerns. **Does not reference `Microsoft.AspNetCore.*`.** There is deliberately **no `Services/` folder** — cross-cutting technical helpers belong in `hhh.infrastructure`, not here.
- **`hhh.webapi.admin`** — thin HTTP boundary. Controllers **never** directly inject or use `XoopsContext` (or any DbContext). All DB operations must go through an application-layer service (`I*Service` → `*Service` in `hhh.application.admin`). Even simple single-table CRUD requires a service — controllers only translate HTTP requests into service calls and map the service's `*Result.Code` into HTTP status codes via `ApiResponse<T>`. `Program.cs` wires JWT validation, CORS, Swagger Bearer and calls `AddInfrastructure(builder.Configuration)` + `AddAdminApplication()`.

## Key conventions

### Naming
All new projects use **dot notation** (`hhh.<layer>.<scope>`). Do not introduce hyphens. The legacy `hhh-backend` name survives only in `hhh-backend.sln`.

### Unified response envelope (hard rule)
**Every** response body on this API is `ApiResponse<T>` from `hhh.api.contracts/Common/ApiResponse.cs`:

```json
{ "code": 200, "message": "success", "data": { ... } }
```

There is only one public type: the generic `ApiResponse<T>`. The non-generic `ApiResponse` is a **static helper** (`ApiResponse.Ok()`, `ApiResponse.Error(code, msg)`) that returns `ApiResponse<object>` — this keeps Swagger from generating two schemas for the error shape. Do not reintroduce a non-generic class.

Success / expected-failure pattern (controllers never throw for expected failures):

```csharp
var result = await _authService.LoginAsync(request, ct);
if (!result.IsSuccess)
    return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
return Ok(ApiResponse<LoginResponse>.Success(result.Data!, result.Message));
```

**Success path rules** (apply to every controller action, not just Auth):

1. Always build the body with the static factory — `ApiResponse<T>.Success(...)` for 200, `ApiResponse<T>.Created(...)` for 201. **Never** `new ApiResponse<T> { Code = ..., Message = ..., Data = ... }` inline. If 201/204 isn't covered by a factory, add a new factory rather than hand-rolling the object.
2. The `message` argument always comes from the service's `result.Message`. **Do not** hardcode Chinese strings like `"更新成功"` in the controller — put them as the default on the domain `*Result` helper (e.g. `UserMutationResult.Ok(uid, message = "更新成功")`) so the controller stays HTTP-only and the text is owned by the use case.
3. For 201 Created, pair the factory with `StatusCode(StatusCodes.Status201Created, ApiResponse<object>.Created(...))`. Do not use `Ok(...)` for a create.

Canonical 201 example:

```csharp
var result = await _userService.CreateAsync(request, ct);
if (!result.IsSuccess)
    return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
return StatusCode(
    StatusCodes.Status201Created,
    ApiResponse<object>.Created(new { id = result.UserId }, result.Message));
```

The format is enforced at **five** pipeline points — do not bypass or re-implement any of them:

| Failure source | Where it's converted | Envelope code |
|---|---|---|
| Model validation (DataAnnotations) | `ConfigureApiBehaviorOptions.InvalidModelStateResponseFactory` in `Program.cs` | 400 |
| `[Authorize]` / invalid or missing JWT | `JwtBearerEvents.OnChallenge` in `Program.cs` | 401 |
| `[Authorize]` policy failure | `JwtBearerEvents.OnForbidden` in `Program.cs` | 403 |
| Unmatched route / 405 / other empty-body status codes | `app.UseStatusCodePages(...)` in `Program.cs` | matches HTTP status |
| Any unhandled exception | `app.UseExceptionHandler(...)` in `Program.cs` | 500 (message is hidden outside Development) |

Controllers should still declare each possible status via `[ProducesResponseType(...)]` so Swagger shows them — the runtime body is guaranteed by `Program.cs` regardless. To avoid repeating the four common error attributes on every controller, all controllers inherit from **`ApiControllerBase`** (`hhh.webapi.admin/Controllers/ApiControllerBase.cs`), which carries class-level `[ApiController]`, `[Produces("application/json")]`, and `[ProducesResponseType(typeof(ApiResponse<object>), ...)]` for 400 / 401 / 403 / 500. Derived controllers only need to declare their own `[Route]`, optional `[Authorize]`, and the 2xx success type on each action.

### RESTful routing (hard rule for every admin endpoint)
**Every endpoint under `hhh.webapi.admin` must follow RESTful conventions.** The URL describes **which resource**, the HTTP verb describes **what to do**. Do not put actions in the URL.

**Resource naming**
- Always **plural, lowercase** nouns: `/api/users`, `/api/orders`, `/api/banners`.
- Multi-word segments use **kebab-case**: `/api/banner-clients`, `/api/builder-products`. Do not use camelCase or PascalCase in URLs even though C# class names are PascalCase.
- `[Route("api/[controller]")]` is allowed — the `[controller]` token resolves to the pluralised controller name (e.g. `UsersController` → `users`). If the resulting URL is wrong, override with an explicit string `[Route("api/banner-clients")]`.
- C# method names are free — they do **not** appear in URLs. `GetList`, `Index`, `Find` are all fine because the `[HttpGet(...)]` attribute decides the route.

**Standard CRUD mapping** — implement these before adding anything custom:

| Intent | HTTP + URL | Controller action |
|---|---|---|
| List (paged, filtered) | `GET /api/users` | `[HttpGet]` |
| Single item | `GET /api/users/{id:int}` | `[HttpGet("{id:int}")]` |
| Create | `POST /api/users` | `[HttpPost]` |
| Full update | `PUT /api/users/{id:int}` | `[HttpPut("{id:int}")]` |
| Partial update | `PATCH /api/users/{id:int}` | `[HttpPatch("{id:int}")]` |
| Delete | `DELETE /api/users/{id:int}` | `[HttpDelete("{id:int}")]` |

**Search, filter, sort, paging** all go into query string on the list endpoint — never create a separate `/search` endpoint:

```
GET /api/users?keyword=john&status=active&page=1&pageSize=20&sort=id&by=DESC
```

**Banned patterns** (will be rejected in review):

| Do NOT write | Write instead |
|---|---|
| `GET /api/users/getList` | `GET /api/users` |
| `GET /api/getUserById?id=5` | `GET /api/users/5` |
| `POST /api/users/create` | `POST /api/users` |
| `POST /api/deleteUser?id=5` | `DELETE /api/users/5` |
| `/api/user` (singular) | `/api/users` (plural) |
| `/api/UserList` / `/api/userList` | `/api/users` |

**Non-CRUD operations** (e.g. stop/start, approve, send email, reset password, cancel order): treat them as a sub-resource or, if that really does not fit, a **verb suffix in kebab-case** on a specific resource instance. Verbs only at the end of the path, never in the middle.

```
POST   /api/users/{id:int}/deactivate             # 停用帳號
POST   /api/users/{id:int}/password-reset         # 重設密碼
POST   /api/orders/{id:int}/cancel                # 取消訂單
POST   /api/banners/{id:int}/publish              # 上架
```

**Sanctioned exception — auth / session endpoints.** `/api/auth/*` is allowed to use the controller/action style because "login" and "logout" do not map to any single resource. Keep this exception limited to authentication; do not extend it to other controllers.

```
POST /api/auth/login       ← OK (sanctioned exception)
POST /api/auth/logout      ← OK
POST /api/auth/refresh     ← OK
POST /api/users/login      ← NOT OK: put it under /api/auth
```

**Status codes** — pair them with the envelope code so both agree:

| Scenario | HTTP | Envelope `code` |
|---|---|---|
| List / single item fetched | 200 | 200 |
| Created successfully | 201 (preferred) or 200 | matches HTTP |
| Deleted successfully, nothing to return | 204 or 200 with `data: null` | matches HTTP |
| Validation failed | 400 | 400 |
| Auth missing/invalid | 401 | 401 |
| Forbidden by policy | 403 | 403 |
| Resource not found | 404 | 404 |
| Conflict (e.g. duplicate key) | 409 | 409 |
| Unhandled exception | 500 | 500 |

### Feature folders, not "Services"
Application-layer code is organised by feature (`Auth/`), not by technical role. When adding a new endpoint family:

1. Create a feature folder under `hhh.application.admin/<Feature>/`
2. Add `I<Feature>Service`, `<Feature>Service`, and any `*Result` types
3. Add request/response DTOs under `hhh.api.contracts.admin/<Feature>/`
4. Register the service in `hhh.application.admin/DependencyInjection.cs` → `AddAdminApplication()`
5. Add a thin controller in `hhh.webapi.admin/Controllers/`

### JWT
- **Generation**: `IJwtTokenGenerator` in `hhh.infrastructure/Auth/`. Takes a primitive `subject` string and an `IEnumerable<Claim>` — it **does not** know about `Admin` or any business entity. Business code (e.g. `AuthService`) is responsible for flattening its entity into claims before calling it.
- **Validation**: `Microsoft.AspNetCore.Authentication.JwtBearer` is configured in `hhh.webapi.admin/Program.cs` only.
- **Settings**: `appsettings.json` → `Jwt` section (`Issuer`, `Audience`, `SecretKey`, `ExpireMinutes`). `SecretKey` must be replaced before deploying anywhere real.
- Registration: `IJwtTokenGenerator` is registered as **Singleton** inside `AddInfrastructure()`. Do not register it again in the application layer.

### Legacy password comparison
`AuthService.LoginAsync` intentionally uses a plain string compare against `admin.Pwd` (varchar(40)) to stay compatible with the legacy PHP system. There is a `// 注意` comment flagging this. If you migrate to BCrypt/Argon2, update both the service and perform a data migration — don't silently change one side.

### `XoopsContext`
- Scaffolded from the live MySQL database. Regenerating it will overwrite hand edits — avoid editing it directly.
- Connection string comes from `appsettings.json` → `ConnectionStrings:Xoops`, server version from `MySql:Version`. `AddInfrastructure(IConfiguration)` reads both and wires `UseMySql(... , ServerVersion.Parse(...), x => x.UseNetTopologySuite())`.
- The scaffolded `OnConfiguring` method has been **deleted** (along with its hardcoded credentials and the `#warning`). **Future scaffolds must pass `--no-onconfiguring`** or the hardcoded version will come back.
- Entities live under `hhh.infrastructure/Dto/Xoops/` (namespace `hhh.infrastructure.Dto.Xoops`). The parent `Dto/` folder is kept for historical reasons but these are EF entities, not request/response DTOs. When a second database is added its entities go under `Dto/<DbName>/`.

## Gotchas

### PowerShell file operations on .cs files with Chinese content
When writing PowerShell scripts that read/write .cs files, **always** specify `-Encoding UTF8` on both `Get-Content` and `Set-Content`. Windows PowerShell 5.x defaults to the system ANSI code page and will corrupt CJK comments:

```powershell
# WRONG — corrupts Chinese comments
$content = Get-Content -LiteralPath $path -Raw

# RIGHT
$content = Get-Content -LiteralPath $path -Raw -Encoding UTF8
Set-Content  -LiteralPath $path -Value $content -NoNewline -Encoding UTF8
```

Symptom is mojibake like `蝞∠?敺?餃隢?`. For rename/namespace updates touching a small number of files, prefer the Edit tool over PowerShell scripts — it handles encoding correctly.

### Scaffold regeneration
Re-scaffolding `XoopsContext` with a plain `dotnet ef dbcontext scaffold ...` command will re-insert `OnConfiguring` with the hardcoded credentials and re-emit the `CS1030` warning. Always pass `--no-onconfiguring` when scaffolding, and place entities under `Dto/Xoops/` with namespace `hhh.infrastructure.Dto.Xoops`.
