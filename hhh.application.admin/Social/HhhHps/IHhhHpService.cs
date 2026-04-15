using hhh.api.contracts.admin.Social.HhhHps;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Social.HhhHps;

/// <summary>HHH HP 列表服務（唯讀）</summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../third/v1/Calculator.php → requesthpindex_get
///             → Hp_model::requestget()
/// 後台 view:hhh-backstage/.../event/hhh_hp.php
/// </remarks>
public interface IHhhHpService
{
    /// <summary>取得 HP 列表（分頁,支援日期區間 + 關鍵字搜尋）</summary>
    Task<PagedResponse<HhhHpListItem>> GetListAsync(HhhHpListQuery query, CancellationToken ct = default);
}
