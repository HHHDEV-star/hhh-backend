using hhh.application.admin.Admins;
using hhh.application.admin.Auth;
using hhh.application.admin.CalculatorRequests;
using hhh.application.admin.Hawards;
using hhh.application.admin.Hcases;
using hhh.application.admin.Hdesigners;
using hhh.application.admin.Hprizes;
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
        services.AddScoped<IHcaseService, HcaseService>();
        services.AddScoped<IHawardService, HawardService>();
        services.AddScoped<IHprizeService, HprizeService>();
        services.AddScoped<ICalculatorRequestService, CalculatorRequestService>();
        return services;
    }
}
