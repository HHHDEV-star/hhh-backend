using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Topics;

/// <summary>
/// 新增主題請求
/// （對應舊版 PHP:_htopic_edit.php POST action=1, id 空白時為新增）
/// </summary>
public class CreateHtopicRequest
{
    /// <summary>主題名稱（前台限 15 字,DB 允許 64）</summary>
    [Required]
    [StringLength(64)]
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>logo 路徑</summary>
    [StringLength(64)]
    public string? Logo { get; set; }

    /// <summary>FB 分享圖片路徑</summary>
    [StringLength(128)]
    public string? SeoImage { get; set; }

    /// <summary>FB 分享標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    /// <summary>FB 分享敘述</summary>
    public string? SeoDescription { get; set; }

    /// <summary>個案 IDs（逗號分隔）</summary>
    [StringLength(128)]
    public string? StrarrHcaseId { get; set; }

    /// <summary>專欄 IDs（逗號分隔）</summary>
    [StringLength(128)]
    public string? StrarrHcolumnId { get; set; }

    /// <summary>上線狀態</summary>
    [Required]
    public bool Onoff { get; set; }
}
