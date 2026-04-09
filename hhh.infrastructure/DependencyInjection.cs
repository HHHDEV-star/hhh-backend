using hhh.infrastructure.Auth;
using hhh.infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace hhh.infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// 註冊 Infrastructure 層服務（DbContext、JWT token 產生器等技術實作）
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // 使用 XoopsContext 內建的 OnConfiguring 連線設定
        services.AddDbContext<XoopsContext>();

        // JWT token 產生器（純技術實作，跨 application 層共用）
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
