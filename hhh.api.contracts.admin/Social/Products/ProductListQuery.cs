using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Products;

/// <summary>產品後台列表查詢條件</summary>
public class ProductListQuery : PagedRequest
{
    /// <summary>模糊比對產品名稱</summary>
    public string? Keyword { get; set; }

    /// <summary>上線狀態（0:關 1:開）</summary>
    public byte? Onoff { get; set; }

    /// <summary>分類1篩選</summary>
    public string? Cate1 { get; set; }

    /// <summary>廠商ID篩選</summary>
    public uint? HbrandId { get; set; }
}
