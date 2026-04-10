using hhh.infrastructure.Auth;
using hhh.infrastructure.Context;
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
        // Xoops (legacy) DbContext — 連線字串從 appsettings 讀取
        var xoopsConn = configuration.GetConnectionString("Xoops")
            ?? throw new InvalidOperationException("ConnectionStrings:Xoops is not configured.");

        var mysqlVersionRaw = configuration["MySql:Version"] ?? "8.0.45-mysql";
        var serverVersion = ServerVersion.Parse(mysqlVersionRaw);

        services.AddDbContext<XoopsContext>(options =>
            options.UseMySql(xoopsConn, serverVersion, mysql => mysql.UseNetTopologySuite()));

        // JWT token 產生器（純技術實作，跨 application 層共用）
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
