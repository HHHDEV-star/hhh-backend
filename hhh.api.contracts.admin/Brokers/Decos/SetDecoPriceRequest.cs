using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 付款通知
/// （對應舊版 PHP:Renovation.php → set_price_post → Renovation_model::set_price）
/// </summary>
/// <remarks>
/// 業務規則:
/// <list type="bullet">
///   <item>更新 deco_price / proposal_price / deco_set_price。</item>
///   <item>PHP 原本會寄信給客戶 + agent@hhh.com.tw + technical_director,目前 .NET 尚未整合 SMTP,暫改為寫 _hoplog 代替,待 SMTP 接通後再補。</item>
/// </list>
/// </remarks>
public class SetDecoPriceRequest
{
    /// <summary>丈量費</summary>
    [Range(0, uint.MaxValue)]
    public uint DecoSetPrice { get; set; }

    /// <summary>提案費</summary>
    public int ProposalPrice { get; set; }

    /// <summary>本次收款金額</summary>
    [Range(0, uint.MaxValue)]
    public uint DecoPrice { get; set; }
}
