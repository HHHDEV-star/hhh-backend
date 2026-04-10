using hhh.infrastructure.Auth;
using hhh.infrastructure.Context;
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

        // JWT token 產生器（純技術實作，跨 application 層共用）
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // 檔案上傳（本機磁碟實作）
        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.SectionName));
        services.AddSingleton<IImageUploadService, LocalImageUploadService>();

        return services;
    }
}
