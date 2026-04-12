using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Social.Products;

/// <summary>
/// 更新產品請求(id 走 URL)
/// (對應舊版 Product/index_put → product_model::update_product_data)
/// 舊版必填:onoff, cate1, cate2, id
/// </summary>
public class UpdateProductRequest
{
    [Required] public bool Onoff { get; set; }
    [Required] [StringLength(64)] public string Cate1 { get; set; } = string.Empty;
    [Required] [StringLength(64)] public string Cate2 { get; set; } = string.Empty;
    [StringLength(64)] public string? Cate3 { get; set; }
    [StringLength(64)] public string? Space { get; set; }
}
