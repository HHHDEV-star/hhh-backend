using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 批次軟刪除軟裝需求單
/// （對應舊版 PHP:Renovation.php → delete_put → Renovation_model::set_delete）
/// </summary>
/// <remarks>
/// 實作為軟刪除:將 deco_request.is_delete 由 'N' 更新為 'Y'。
/// </remarks>
public class BatchDeleteDecoRequest
{
    /// <summary>要軟刪除的 seq 清單</summary>
    [Required]
    [MinLength(1, ErrorMessage = "seqs 不得為空")]
    public List<int> Seqs { get; set; } = [];
}

/// <summary>
/// 批次刪除附件
/// （對應舊版 PHP:Renovation.php → deco_files_delete → Renovation_model::del_deco_request_files）
/// </summary>
public class BatchDeleteDecoRequestFiles
{
    /// <summary>要刪除的 deco_file_id 清單</summary>
    [Required]
    [MinLength(1, ErrorMessage = "fileIds 不得為空")]
    public List<uint> FileIds { get; set; } = [];
}
