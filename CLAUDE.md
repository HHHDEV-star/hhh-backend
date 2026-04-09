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
- **`hhh.infrastructure`** — owns `XoopsContext` and all 150+ scaffolded entities in `Dto/`, plus non-business technical implementations like `Auth/JwtTokenGenerator`. May reference `Microsoft.Extensions.*` abstractions. Do not put business rules here.
- **`hhh.application.admin`** — use cases written as feature folders (`Auth/`, and future `Users/`, `Orders/`, …). Each feature owns its service interface, implementation, and a **domain `*Result` type** (see `LoginResult`) that represents success/failure without HTTP concerns. **Does not reference `Microsoft.AspNetCore.*`.** There is deliberately **no `Services/` folder** — cross-cutting technical helpers belong in `hhh.infrastructure`, not here.
- **`hhh.webapi.admin`** — thin HTTP boundary. Controllers translate requests into service calls and map the service's `*Result.Code` into HTTP status codes via `ApiResponse<T>`. `Program.cs` wires JWT validation, CORS, Swagger Bearer and calls `AddInfrastructure()` + `AddAdminApplication()`.

## Key conventions

### Naming
All new projects use **dot notation** (`hhh.<layer>.<scope>`). Do not introduce hyphens. The legacy `hhh-backend` name survives only in `hhh-backend.sln`.

### Unified response envelope
Every API endpoint returns `ApiResponse<T>` from `hhh.api.contracts/Common/ApiResponse.cs`:

```json
{ "code": 200, "message": "success", "data": { ... } }
```

Controllers never throw for "expected" failures (bad credentials, disabled account, validation). Services return a domain result; the controller maps the code:

```csharp
var result = await _authService.LoginAsync(request, ct);
if (!result.IsSuccess)
    return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
return Ok(ApiResponse<LoginResponse>.Success(result.Data!, result.Message));
```

Model-state validation failures are also converted to `ApiResponse<object>` with `Code = 400` via `ConfigureApiBehaviorOptions` in `Program.cs`. Do not re-implement this per-controller.

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
- Currently calls `OnConfiguring` with a **hardcoded connection string** (baked in by the scaffolder). This is the source of the single `CS1030 #warning` at build time; leave that warning alone until the connection string is externalised.
- Registered via `services.AddDbContext<XoopsContext>()` with **no options** in `AddInfrastructure()` — this lets `OnConfiguring` take over. Don't pass `UseMySql(...)` here or you'll get a double-configure.
- Entities live under `hhh.infrastructure/Dto/` (namespace `hhh.infrastructure.Dto`). The folder is called `Dto` for historical reasons but these are EF entities, not request/response DTOs.

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

### Stale root-level artifacts
The repo root still contains leftover `bin/Debug/` and `obj/Debug/` directories and an empty `hhh-admin/obj/` folder from the previous project rename (`hhh-backend` → `hhh-admin` → `hhh.webapi.admin`). These are not referenced by any csproj and can be safely deleted if they get in the way; they do not affect builds.
