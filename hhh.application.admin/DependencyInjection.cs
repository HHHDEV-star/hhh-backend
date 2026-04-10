using hhh.application.admin.Admins;
using hhh.application.admin.Auth;
using hhh.application.admin.Hdesigners;
using hhh.application.admin.Users;
using Microsoft.Extensions.DependencyInjection;

namespace hhh.application.admin;

public static class DependencyInjection
{
    /// <summary>
    /// 註冊後台 Application 層服務
    /// </summary>
    public static IServiceCollection AddAdminApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IHdesignerService, HdesignerService>();
        return services;
    }
}
