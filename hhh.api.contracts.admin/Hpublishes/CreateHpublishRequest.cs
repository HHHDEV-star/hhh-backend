using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hpublishes;

/// <summary>
/// 新增出版請求（POST /api/hpublishes，application/json）。
/// </summary>
/// <remarks>
/// 對應舊版 _hpublish_edit.php 的新增分支。
/// 注意：
///  - hpublish_id 由 DB 自動遞增，不開放 client 指定（舊 PHP 有欄位但是 footgun）。
///  - viewed 由前台訪問統計累加，不開放寫入。
/// </remarks>
public class CreateHpublishRequest
{
    /// <summary>書籍類別</summary>
    [Required(ErrorMessage = "書籍類別必填")]
    [StringLength(32, ErrorMessage = "書籍類別長度不得超過 32 字元")]
    public string Type { get; set; } = string.Empty;

    /// <summary>logo 檔名（相對於舊站 gs/publish/ 目錄，字串而非檔案上傳）</summary>
    [StringLength(64, ErrorMessage = "logo 長度不得超過 64 字元")]
    public string? Logo { get; set; }

    /// <summary>名稱（書名）</summary>
    [Required(ErrorMessage = "書名必填")]
    [StringLength(128, ErrorMessage = "書名長度不得超過 128 字元")]
    public string Title { get; set; } = string.Empty;

    /// <summary>作者</summary>
    [Required(ErrorMessage = "作者必填")]
    [StringLength(64, ErrorMessage = "作者長度不得超過 64 字元")]
    public string Author { get; set; } = string.Empty;

    /// <summary>出版日期</summary>
    [Required(ErrorMessage = "出版日期必填")]
    public DateOnly Pdate { get; set; }

    /// <summary>描述</summary>
    public string? Desc { get; set; }

    /// <summary>編輯者備註 / HTML</summary>
    public string? Editor { get; set; }

    /// <summary>推薦數（預設 0）</summary>
    [Range(0, uint.MaxValue, ErrorMessage = "推薦數必須 >= 0")]
    public uint Recommend { get; set; } = 0;
}
