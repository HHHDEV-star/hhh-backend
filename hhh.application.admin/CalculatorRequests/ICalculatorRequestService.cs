using hhh.api.contracts.admin.CalculatorRequests;

namespace hhh.application.admin.CalculatorRequests;

/// <summary>
/// 後台裝修計算機需求服務
/// (對應舊版 hhh-api/application/controllers/third/v1/Calculator.php)
/// </summary>
public interface ICalculatorRequestService
{
    /// <summary>
    /// 取得裝修計算機需求列表
    /// (對應舊版 Calculator/requestindex_get → Calculator_model::requestget)
    /// </summary>
    Task<CalculatorRequestListResponse> GetListAsync(
        CalculatorRequestListQuery query,
        CancellationToken cancellationToken = default);
}
