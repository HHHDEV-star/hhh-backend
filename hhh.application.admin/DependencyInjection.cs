using hhh.application.admin.Auth;
using hhh.application.admin.Awards.Hawards;
using hhh.application.admin.Awards.Hcontests;
using hhh.application.admin.Awards.Hprizes;
using hhh.application.admin.Brokers.Calculators;
using hhh.application.admin.Brokers.CalculatorRequests;
using hhh.application.admin.Brokers.Renovations;
using hhh.application.admin.Content.Hpublishes;
using hhh.application.admin.Content.Htopic2s;
using hhh.application.admin.Designers.Hcases;
using hhh.application.admin.Designers.Hdesigners;
using hhh.application.admin.Editorial.Cases;
using hhh.application.admin.Editorial.Columns;
using hhh.application.admin.Members.Users;
using hhh.application.admin.Platform.Admins;
using hhh.application.admin.Platform.OperationLogs;
using hhh.application.admin.Reports.VideoReports;
using Microsoft.Extensions.DependencyInjection;

namespace hhh.application.admin;

public static class DependencyInjection
{
    /// <summary>
    /// 註冊後台 Application 層服務(依類別資料夾分組)
    /// </summary>
    public static IServiceCollection AddAdminApplication(this IServiceCollection services)
    {
        // Auth (cross-cutting)
        services.AddScoped<IAuthService, AuthService>();

        // Platform - 平台管理
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IOperationLogService, OperationLogService>();

        // Members - 會員管理
        services.AddScoped<IUserService, UserService>();

        // Designers - 設計師
        services.AddScoped<IHdesignerService, HdesignerService>();
        services.AddScoped<IHcaseService, HcaseService>();

        // Editorial - 編輯部
        services.AddScoped<IEditorialCaseService, EditorialCaseService>();
        services.AddScoped<IEditorialColumnService, EditorialColumnService>();

        // Awards - 競賽獎項
        services.AddScoped<IHawardService, HawardService>();
        services.AddScoped<IHprizeService, HprizeService>();
        services.AddScoped<IHcontestService, HcontestService>();

        // Content - 內容
        services.AddScoped<IHpublishService, HpublishService>();
        services.AddScoped<IHtopic2Service, Htopic2Service>();

        // Reports - 報表
        services.AddScoped<IVideoReportService, VideoReportService>();

        // Brokers - 經紀人
        services.AddScoped<ICalculatorService, CalculatorService>();
        services.AddScoped<ICalculatorRequestService, CalculatorRequestService>();
        services.AddScoped<IRenovationService, RenovationService>();

        return services;
    }
}
