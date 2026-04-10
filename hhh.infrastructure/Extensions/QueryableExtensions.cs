using hhh.api.contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Extensions;

/// <summary>
/// <see cref="IQueryable{T}"/> 的共用擴充方法,主要用於把 EF Core 查詢
/// 包成統一格式的分頁回應,避免每個 service 都手寫
/// LongCountAsync + Skip + Take 這組樣板。
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// 把已經 Where / OrderBy / Select 完的 IQueryable 轉成 <see cref="PagedResponse{T}"/>。
    /// </summary>
    /// <remarks>
    /// 呼叫前請先:
    ///   1. 套用所有 Where 條件
    ///   2. 套用排序(OrderBy/ThenBy)—— 沒排序的分頁結果不穩定
    ///   3. 投影到目標型別(.Select(x => new XxxListItem { ... }))
    /// 本方法內部會打 <see cref="EntityFrameworkQueryableExtensions.LongCountAsync{TSource}(IQueryable{TSource}, CancellationToken)"/>
    /// 取總數,再 Skip/Take 取當頁資料,共兩次 round-trip。
    /// </remarks>
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var total = await source.LongCountAsync(cancellationToken);
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResponse<T>
        {
            Items = items,
            Total = total,
            Page = page,
            PageSize = pageSize,
        };
    }
}
