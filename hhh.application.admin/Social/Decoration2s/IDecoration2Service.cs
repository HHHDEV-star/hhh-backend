using hhh.api.contracts.admin.Social.Decoration2s;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Social.Decoration2s;

/// <summary>全室裝修收名單服務（唯讀，資料來自外部 API）</summary>
/// <remarks>
/// 對應舊版 PHP:hhh-backstage/.../event/decoration2.php
/// 原始資料來源：https://q.ptt.cx/v_lst（外部 API）
/// 舊版 Kendo Grid 為前端分頁（pageSize:50），此處改為後端分頁
/// </remarks>
public interface IDecoration2Service
{
    /// <summary>取得全室裝修收名單（分頁）</summary>
    Task<PagedResponse<Decoration2ListItem>> GetListAsync(
        ListQuery query, CancellationToken ct = default);
}
