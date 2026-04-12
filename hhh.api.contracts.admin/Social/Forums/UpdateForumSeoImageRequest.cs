namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 更新討論區 SEO 圖片請求(article_id 走 URL)
/// (對應舊版 Forum/seoimage_put → forum_model::update_forum_seo_image)
/// </summary>
public class UpdateForumSeoImageRequest
{
    /// <summary>SEO 圖片 URL</summary>
    public string? SeoImage { get; set; }
}
