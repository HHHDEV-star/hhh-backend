namespace hhh.api.contracts.admin.Social.Products;

/// <summary>後台產品列表項目(對應 product_model::get_product_lists)</summary>
public class ProductListItem
{
    public uint Id { get; set; }
    public uint HbrandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cate1 { get; set; } = string.Empty;
    public string Cate2 { get; set; } = string.Empty;
    public string Cate3 { get; set; } = string.Empty;
    public string Space { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public bool Onoff { get; set; }
}
