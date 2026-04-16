using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 批次新增軟裝需求單附件
/// （對應舊版 PHP:Renovation.php → deco_files_post → Renovation_model::insert_deco_request_files_by_backstage）
/// </summary>
/// <remarks>
/// 此 API 只負責寫入 DB 關聯紀錄;檔案本體由前端/其他服務處理,
/// 後台前端上傳時會先取得已上傳檔案的 file_name,再呼叫此 API 建立 meta。
/// </remarks>
public class CreateDecoRequestFilesRequest
{
    /// <summary>要建立的附件清單(批次)</summary>
    [Required]
    [MinLength(1, ErrorMessage = "files 不得為空")]
    public List<CreateDecoRequestFileItem> Files { get; set; } = [];
}

/// <summary>單筆附件建立資料</summary>
public class CreateDecoRequestFileItem
{
    [Required]
    [StringLength(255)]
    public string OrigName { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = null!;
}
