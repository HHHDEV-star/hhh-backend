using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Htopic2s;

/// <summary>
/// 新增議題 2 請求(POST /api/htopic2s,application/json)。
/// </summary>
/// <remarks>
/// 對應舊版 _htopic2_edit.php 的新增分支。
/// 注意:id 由 DB 自動遞增,不開放 client 指定(舊 PHP 表單可手填是 footgun)。
/// strarr_* 欄位皆為逗號分隔的 ID 字串,格式由 client 負責。
/// </remarks>
public class CreateHtopic2Request
{
    /// <summary>名稱</summary>
    [Required(ErrorMessage = "名稱必填")]
    [StringLength(128, ErrorMessage = "名稱長度不得超過 128 字元")]
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string? Desc { get; set; }

    /// <summary>logo 檔名(相對於舊站 gs/topic2/ 目錄,字串而非檔案上傳)</summary>
    [StringLength(64, ErrorMessage = "logo 長度不得超過 64 字元")]
    public string? Logo { get; set; }

    /// <summary>設計師 ID CSV(例:"12,34,56")</summary>
    public string? StrarrHdesignerId { get; set; }

    /// <summary>個案 ID CSV</summary>
    public string? StrarrHcaseId { get; set; }

    /// <summary>影音 ID CSV</summary>
    public string? StrarrHvideoId { get; set; }

    /// <summary>專欄 ID CSV</summary>
    public string? StrarrHcolumnId { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; } = false;
}
