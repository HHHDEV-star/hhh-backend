using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using hhh.infrastructure.Dto;

namespace hhh.infrastructure.Context;

public partial class XoopsContext : DbContext
{
    public XoopsContext()
    {
    }

    public XoopsContext(DbContextOptions<XoopsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AgentFile> AgentFiles { get; set; }

    public virtual DbSet<AgentForm> AgentForms { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Avatar> Avatars { get; set; }

    public virtual DbSet<AvatarUserLink> AvatarUserLinks { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BannerSetup> BannerSetups { get; set; }

    public virtual DbSet<Bannerclient> Bannerclients { get; set; }

    public virtual DbSet<Bannerfinish> Bannerfinishes { get; set; }

    public virtual DbSet<BkDesignerJared> BkDesignerJareds { get; set; }

    public virtual DbSet<BkYoutubeList> BkYoutubeLists { get; set; }

    public virtual DbSet<BlockModuleLink> BlockModuleLinks { get; set; }

    public virtual DbSet<Brief> Briefs { get; set; }

    public virtual DbSet<Builder> Builders { get; set; }

    public virtual DbSet<BuilderProduct> BuilderProducts { get; set; }

    public virtual DbSet<CacheModel> CacheModels { get; set; }

    public virtual DbSet<Calculator> Calculators { get; set; }

    public virtual DbSet<CalculatorRequest> CalculatorRequests { get; set; }

    public virtual DbSet<CallinDatum> CallinData { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Configcategory> Configcategories { get; set; }

    public virtual DbSet<Configoption> Configoptions { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<DecoRecord> DecoRecords { get; set; }

    public virtual DbSet<DecoRecordImg> DecoRecordImgs { get; set; }

    public virtual DbSet<DecoRecordPerson> DecoRecordPeople { get; set; }

    public virtual DbSet<DecoRequest> DecoRequests { get; set; }

    public virtual DbSet<DecoRequestFile> DecoRequestFiles { get; set; }

    public virtual DbSet<DecoqueryHistory> DecoqueryHistories { get; set; }

    public virtual DbSet<Decoration> Decorations { get; set; }

    public virtual DbSet<DesignerBranch> DesignerBranches { get; set; }

    public virtual DbSet<DesignerKeywordSearch> DesignerKeywordSearches { get; set; }

    public virtual DbSet<EdmLog> EdmLogs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<ExecuteDetail> ExecuteDetails { get; set; }

    public virtual DbSet<ExecuteFile> ExecuteFiles { get; set; }

    public virtual DbSet<ExecuteForm> ExecuteForms { get; set; }

    public virtual DbSet<ExecuteItem> ExecuteItems { get; set; }

    public virtual DbSet<ExecuteSignOff> ExecuteSignOffs { get; set; }

    public virtual DbSet<Forget> Forgets { get; set; }

    public virtual DbSet<ForumArticle> ForumArticles { get; set; }

    public virtual DbSet<ForumArticleReply> ForumArticleReplies { get; set; }

    public virtual DbSet<ForumArticleReport> ForumArticleReports { get; set; }

    public virtual DbSet<ForumArticleReview> ForumArticleReviews { get; set; }

    public virtual DbSet<ForumTrack> ForumTracks { get; set; }

    public virtual DbSet<Forward> Forwards { get; set; }

    public virtual DbSet<GoStorage> GoStorages { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }

    public virtual DbSet<GroupsUsersLink> GroupsUsersLinks { get; set; }

    public virtual DbSet<HLineRss> HLineRsses { get; set; }

    public virtual DbSet<Had> Hads { get; set; }

    public virtual DbSet<Haward> Hawards { get; set; }

    public virtual DbSet<Hbrand> Hbrands { get; set; }

    public virtual DbSet<HbrandPage> HbrandPages { get; set; }

    public virtual DbSet<Hcase> Hcases { get; set; }

    public virtual DbSet<Hcase0511> Hcase0511s { get; set; }

    public virtual DbSet<HcaseImg> HcaseImgs { get; set; }

    public virtual DbSet<HcaseImg0511> HcaseImg0511s { get; set; }

    public virtual DbSet<Hclick> Hclicks { get; set; }

    public virtual DbSet<Hcolumn> Hcolumns { get; set; }

    public virtual DbSet<HcolumnImg> HcolumnImgs { get; set; }

    public virtual DbSet<HcolumnPage> HcolumnPages { get; set; }

    public virtual DbSet<HcomparisonTbl> HcomparisonTbls { get; set; }

    public virtual DbSet<Hcontest> Hcontests { get; set; }

    public virtual DbSet<HcontestForum> HcontestForums { get; set; }

    public virtual DbSet<HcontestVote> HcontestVotes { get; set; }

    public virtual DbSet<Hdesigner> Hdesigners { get; set; }

    public virtual DbSet<HdesignerAddtion> HdesignerAddtions { get; set; }

    public virtual DbSet<HdesignerTmp> HdesignerTmps { get; set; }

    public virtual DbSet<Hevent> Hevents { get; set; }

    public virtual DbSet<HeventAttend> HeventAttends { get; set; }

    public virtual DbSet<Hext1> Hext1s { get; set; }

    public virtual DbSet<Hguestbook> Hguestbooks { get; set; }

    public virtual DbSet<HhhHp> HhhHps { get; set; }

    public virtual DbSet<Hhp> Hhps { get; set; }

    public virtual DbSet<HmillionAttend> HmillionAttends { get; set; }

    public virtual DbSet<HmillionPrize> HmillionPrizes { get; set; }

    public virtual DbSet<HmillionVendor> HmillionVendors { get; set; }

    public virtual DbSet<HmillionVote> HmillionVotes { get; set; }

    public virtual DbSet<Hmyfav> Hmyfavs { get; set; }

    public virtual DbSet<HmyfavMobile> HmyfavMobiles { get; set; }

    public virtual DbSet<Hnewspaper> Hnewspapers { get; set; }

    public virtual DbSet<HomepageSet> HomepageSets { get; set; }

    public virtual DbSet<Hoplog> Hoplogs { get; set; }

    public virtual DbSet<Hprize> Hprizes { get; set; }

    public virtual DbSet<HprizeEvent> HprizeEvents { get; set; }

    public virtual DbSet<Hproduct> Hproducts { get; set; }

    public virtual DbSet<HproductImg> HproductImgs { get; set; }

    public virtual DbSet<Hprog> Hprogs { get; set; }

    public virtual DbSet<HprogChan> HprogChans { get; set; }

    public virtual DbSet<HprogChanTbl> HprogChanTbls { get; set; }

    public virtual DbSet<HprogTbl> HprogTbls { get; set; }

    public virtual DbSet<HprogUnit> HprogUnits { get; set; }

    public virtual DbSet<Hpublish> Hpublishes { get; set; }

    public virtual DbSet<Hpush> Hpushes { get; set; }

    public virtual DbSet<Htopic> Htopics { get; set; }

    public virtual DbSet<Htopic2> Htopic2s { get; set; }

    public virtual DbSet<Hvideo> Hvideos { get; set; }

    public virtual DbSet<HvideoBak> HvideoBaks { get; set; }

    public virtual DbSet<HvideoTmp> HvideoTmps { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Imagebody> Imagebodies { get; set; }

    public virtual DbSet<Imagecategory> Imagecategories { get; set; }

    public virtual DbSet<Imgset> Imgsets { get; set; }

    public virtual DbSet<ImgsetTplsetLink> ImgsetTplsetLinks { get; set; }

    public virtual DbSet<Imgsetimg> Imgsetimgs { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MobileInnerSetup> MobileInnerSetups { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Newblock> Newblocks { get; set; }

    public virtual DbSet<Online> Onlines { get; set; }

    public virtual DbSet<OuterSiteSet> OuterSiteSets { get; set; }

    public virtual DbSet<PhotosEdm> PhotosEdms { get; set; }

    public virtual DbSet<Precise> Precises { get; set; }

    public virtual DbSet<PrivMsg> PrivMsgs { get; set; }

    public virtual DbSet<ProgList> ProgLists { get; set; }

    public virtual DbSet<ProgVideo> ProgVideos { get; set; }

    public virtual DbSet<Psychological> Psychologicals { get; set; }

    public virtual DbSet<QuotationBrand> QuotationBrands { get; set; }

    public virtual DbSet<QuotationDesigner> QuotationDesigners { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<RenovationReuqest> RenovationReuqests { get; set; }

    public virtual DbSet<RssLinetoday> RssLinetodays { get; set; }

    public virtual DbSet<RssMsn> RssMsns { get; set; }

    public virtual DbSet<RssTransfer> RssTransfers { get; set; }

    public virtual DbSet<RssYahoo> RssYahoos { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopImg> ShopImgs { get; set; }

    public virtual DbSet<ShortUrl> ShortUrls { get; set; }

    public virtual DbSet<ShortUrlLog> ShortUrlLogs { get; set; }

    public virtual DbSet<SiteSetup> SiteSetups { get; set; }

    public virtual DbSet<Smile> Smiles { get; set; }

    public virtual DbSet<SmsHistory> SmsHistories { get; set; }

    public virtual DbSet<TmpTbl> TmpTbls { get; set; }

    public virtual DbSet<Tplfile> Tplfiles { get; set; }

    public virtual DbSet<Tplset> Tplsets { get; set; }

    public virtual DbSet<Tplsource> Tplsources { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFavorite> UserFavorites { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UsersAuth> UsersAuths { get; set; }

    public virtual DbSet<Xoopscomment> Xoopscomments { get; set; }

    public virtual DbSet<Xoopsnotification> Xoopsnotifications { get; set; }

    public virtual DbSet<YoutubeGroup> YoutubeGroups { get; set; }

    public virtual DbSet<YoutubeGroupDetail> YoutubeGroupDetails { get; set; }

    public virtual DbSet<YoutubeList> YoutubeLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=18.177.57.10;port=3306;database=xoops;uid=npa_user;pwd=S!lver2024@DB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"), x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_unicode_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Account).HasComment("帳號");
            entity.Property(e => e.AllowPage).HasComment("允許使用頁面");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("信箱");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasComment("啟用狀態");
            entity.Property(e => e.Name).HasComment("名稱");
            entity.Property(e => e.Pwd).HasComment("密碼");
            entity.Property(e => e.Tel).HasComment("電話");
        });

        modelBuilder.Entity<AgentFile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PRIMARY");
        });

        modelBuilder.Entity<AgentForm>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PRIMARY");

            entity.ToTable("agent_form", tb => tb.HasComment("經紀人表單"));

            entity.Property(e => e.AgentId).HasComment("PK");
            entity.Property(e => e.Address).HasComment("裝修地址");
            entity.Property(e => e.AgentLoan).HasComment("需要申請優惠貸款");
            entity.Property(e => e.AgentNote).HasComment("備註");
            entity.Property(e => e.AgentSource).HasComment("管道來源");
            entity.Property(e => e.AgentWhere).HasComment("何處得知幸福經紀人");
            entity.Property(e => e.AgentWhereOther).HasComment("何處得知幸福經紀人 其他");
            entity.Property(e => e.Budget).HasComment("裝修預算");
            entity.Property(e => e.Cellphone).HasComment("手機");
            entity.Property(e => e.ContentFor).HasComment("聯繫人員");
            entity.Property(e => e.ContentTime).HasComment("聯繫時間");
            entity.Property(e => e.ContentWay).HasComment("初步聯繫方式");
            entity.Property(e => e.County).HasComment("縣市");
            entity.Property(e => e.CustomerNote).HasComment("備註");
            entity.Property(e => e.DateAdded).HasComment("新增時間");
            entity.Property(e => e.DateModified).HasComment("修改時間");
            entity.Property(e => e.DecorationTime).HasComment("預計裝修時間");
            entity.Property(e => e.DesignCompany).HasComment("推薦設計公司");
            entity.Property(e => e.DesignerContent).HasComment("設計師/廠商可否聯繫");
            entity.Property(e => e.District).HasComment("區域");
            entity.Property(e => e.Email).HasComment("EMAIL");
            entity.Property(e => e.Family).HasComment("即將入住成員");
            entity.Property(e => e.FollowTime).HasComment("下次追蹤時間");
            entity.Property(e => e.Fullname).HasComment("屋主姓名");
            entity.Property(e => e.Higher).HasComment("挑高");
            entity.Property(e => e.HouseStatus).HasComment("房屋型態");
            entity.Property(e => e.HouseStatusFloor).HasComment("裝修幾樓	");
            entity.Property(e => e.HouseStatusHigh).HasComment("大樓高");
            entity.Property(e => e.HouseType).HasComment("房屋類型");
            entity.Property(e => e.HouseTypeYear).HasComment("中古幾年");
            entity.Property(e => e.InterviewTime).HasComment("初步面台時間");
            entity.Property(e => e.LocationPingPaper).HasComment("空間坪數 權狀");
            entity.Property(e => e.LocationPingReal).HasComment("空間坪數 實際");
            entity.Property(e => e.MarketRule).HasComment("是否同意行情價3-5 ");
            entity.Property(e => e.MarketRule1).HasComment("是否同意行情價6-12 ");
            entity.Property(e => e.Mbudget).HasComment("提案/丈量費預算");
            entity.Property(e => e.NeedItem).HasComment("需要進行");
            entity.Property(e => e.NeedStyle).HasComment("風格需求");
            entity.Property(e => e.NeedStyleOther).HasComment("其他風格");
            entity.Property(e => e.NeedUpdateArray).HasComment("是否更新 array");
            entity.Property(e => e.Phone).HasComment("市話");
            entity.Property(e => e.PlecementC).HasComment("目前格局 廳");
            entity.Property(e => e.PlecementH).HasComment("目前格局 房");
            entity.Property(e => e.PlecementT).HasComment("目前格局 衛");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("PRIMARY");

            entity.Property(e => e.Aid).HasComment("pk");
            entity.Property(e => e.Content).HasComment("內容");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.EndTime).HasComment("結束時間");
            entity.Property(e => e.Link).HasComment("公告連結");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("上線狀態(N:關Y:開)");
            entity.Property(e => e.Sort).HasComment("排序");
            entity.Property(e => e.StartTime).HasComment("開始時間");
            entity.Property(e => e.UpdateTime).HasComment("修改時間");
        });

        modelBuilder.Entity<Avatar>(entity =>
        {
            entity.HasKey(e => e.AvatarId).HasName("PRIMARY");

            entity.Property(e => e.AvatarFile).HasDefaultValueSql("''");
            entity.Property(e => e.AvatarMimetype).HasDefaultValueSql("''");
            entity.Property(e => e.AvatarName).HasDefaultValueSql("''");
            entity.Property(e => e.AvatarType)
                .HasDefaultValueSql("''")
                .IsFixedLength();
        });

        modelBuilder.Entity<AvatarUserLink>(entity =>
        {
            entity.Property(e => e.AvatarId).HasDefaultValueSql("'0'");
            entity.Property(e => e.UserId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PRIMARY");

            entity.Property(e => e.Clicks).HasDefaultValueSql("'0'");
            entity.Property(e => e.Clickurl).HasDefaultValueSql("''");
            entity.Property(e => e.Imageurl).HasDefaultValueSql("''");
            entity.Property(e => e.Impmade).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<BannerSetup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("banner_setup", tb => tb.HasComment("首頁為主"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("pk");
            entity.Property(e => e.EditorsPicks).HasComment("編輯精選");
            entity.Property(e => e.RecommendBrand).HasComment("特別推薦廠商");
            entity.Property(e => e.RecommendDesigner).HasComment("特別推薦設計師");
            entity.Property(e => e.StrarrHbrandId).HasComment("本週推薦廠商IDs");
            entity.Property(e => e.StrarrHcaseId).HasComment("推薦個案IDs");
            entity.Property(e => e.StrarrHdesignerId).HasComment("本週推薦設計師IDs");
        });

        modelBuilder.Entity<Bannerclient>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PRIMARY");

            entity.Property(e => e.Contact).HasDefaultValueSql("''");
            entity.Property(e => e.Email).HasDefaultValueSql("''");
            entity.Property(e => e.Login).HasDefaultValueSql("''");
            entity.Property(e => e.Name).HasDefaultValueSql("''");
            entity.Property(e => e.Passwd).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Bannerfinish>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PRIMARY");

            entity.Property(e => e.Clicks).HasDefaultValueSql("'0'");
            entity.Property(e => e.Impressions).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<BkDesignerJared>(entity =>
        {
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Area).HasComment("接案坪數");
            entity.Property(e => e.Awards).HasComment("獲獎紀錄");
            entity.Property(e => e.AwardsLogo).HasComment("獎項logo");
            entity.Property(e => e.AwardsName).HasComment("獎項名稱");
            entity.Property(e => e.Background).HasComment("設計師背景圖");
            entity.Property(e => e.BackgroundMobile).HasComment("手機板背景圖");
            entity.Property(e => e.Blog).HasComment("其他網址連結");
            entity.Property(e => e.Budget).HasComment("接案預算");
            entity.Property(e => e.Career).HasComment("相關經歷");
            entity.Property(e => e.Charge).HasComment("收費方式");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CoordinateX).HasComment("座標X");
            entity.Property(e => e.CoordinateY).HasComment("座標Y");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Description).HasComment("header.description");
            entity.Property(e => e.DesignerMail).HasComment("通知設計師的email");
            entity.Property(e => e.Dorder)
                .HasDefaultValueSql("'99999'")
                .HasComment("排序");
            entity.Property(e => e.Fax).HasComment("傳真");
            entity.Property(e => e.Fbpageurl).HasComment("FB page URL");
            entity.Property(e => e.Guarantee).HasComment("幸福經紀人");
            entity.Property(e => e.HdesignerId).HasComment("pk");
            entity.Property(e => e.Idea).HasComment("設計理念");
            entity.Property(e => e.ImgPath).HasComment("頭像");
            entity.Property(e => e.IsSend).HasComment("寄送數目");
            entity.Property(e => e.JsonLd).HasComment("JSON-LD內容");
            entity.Property(e => e.License).HasComment("相關證照");
            entity.Property(e => e.LineLink).HasComment("LINE URL");
            entity.Property(e => e.Location).HasComment("設計師所在區域");
            entity.Property(e => e.Mail).HasComment("E-mail");
            entity.Property(e => e.MaxBudget).HasComment("預算最大值");
            entity.Property(e => e.MetaDescription).HasComment("Meta內容");
            entity.Property(e => e.MinBudget).HasComment("預算最小值");
            entity.Property(e => e.MobileOrder)
                .HasDefaultValueSql("'99999'")
                .HasComment("手機板排序");
            entity.Property(e => e.Name).HasComment("設計師名稱");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Payment).HasComment("付費方式");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Position).HasComment("品牌定位");
            entity.Property(e => e.Region).HasComment("接案區域");
            entity.Property(e => e.SalesMail).HasComment("業務的email");
            entity.Property(e => e.Seo).HasComment("SEO內容");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.Special).HasComment("特殊接案");
            entity.Property(e => e.Style).HasComment("接案風格");
            entity.Property(e => e.Taxid).HasComment("公司統編");
            entity.Property(e => e.Title).HasComment("公司抬頭");
            entity.Property(e => e.Top)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("置頂(N: 否 / Y:是");
            entity.Property(e => e.Type).HasComment("接案類型");
            entity.Property(e => e.Website).HasComment("網站");
            entity.Property(e => e.XoopsUid).HasComment("指定[設計師群組]的會員");
        });

        modelBuilder.Entity<BkYoutubeList>(entity =>
        {
            entity.Property(e => e.BuilderId).HasComment("建商ID");
            entity.Property(e => e.ChannelId).HasComment("頻道編號(Youtube");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Description).HasComment("影片敘述");
            entity.Property(e => e.HbrandId).HasComment("廠商id");
            entity.Property(e => e.HdesignerId).HasComment("設計師pk");
            entity.Property(e => e.IsDel).HasComment("是否刪除(0 : 否 / 1 : 是)");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'Y'")
                .IsFixedLength()
                .HasComment("是否開啟(Y:開 / N:關");
            entity.Property(e => e.PageToken).HasComment("換頁token");
            entity.Property(e => e.PublishedTime).HasComment("影片發布時間");
            entity.Property(e => e.Title).HasComment("影片標題");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
            entity.Property(e => e.Yid).HasComment("pk");
            entity.Property(e => e.YoutubeImg).HasComment("圖片位置");
            entity.Property(e => e.YoutubeVideoId).HasComment("youtube影片id");
        });

        modelBuilder.Entity<BlockModuleLink>(entity =>
        {
            entity.HasKey(e => new { e.ModuleId, e.BlockId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.BlockId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Brief>(entity =>
        {
            entity.HasKey(e => e.BriefId).HasName("PRIMARY");

            entity.ToTable("brief", tb => tb.HasComment("幸福回娘家"));

            entity.Property(e => e.Fee).HasComment("預算");
            entity.Property(e => e.Image).HasComment("名片圖檔");
        });

        modelBuilder.Entity<Builder>(entity =>
        {
            entity.HasKey(e => e.BuilderId).HasName("PRIMARY");

            entity.Property(e => e.BuilderId).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.BackgroundMobile).HasComment("手機背景圖");
            entity.Property(e => e.Border).HasComment("排序");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("品牌描述");
            entity.Property(e => e.Email).HasComment("廠商EMAIL");
            entity.Property(e => e.Fbpageurl).HasComment("Facebook page URL");
            entity.Property(e => e.Gchoice).HasComment("幸福空間推薦");
            entity.Property(e => e.History).HasComment("獲獎紀錄");
            entity.Property(e => e.HvideoId).HasComment("影片ID");
            entity.Property(e => e.Intro).HasComment("品牌介紹");
            entity.Property(e => e.IsSend).HasComment("已寄送次數");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Logo2).HasComment("logo2");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關閉 1:開啟)");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.SubCompany).HasComment("分公司");
            entity.Property(e => e.Title).HasComment("廠商名稱");
            entity.Property(e => e.Vr360Id).HasComment("vr306_ID");
            entity.Property(e => e.Website).HasComment("網站");
        });

        modelBuilder.Entity<BuilderProduct>(entity =>
        {
            entity.HasKey(e => e.BuilderProductId).HasName("PRIMARY");

            entity.Property(e => e.BuilderProductId).HasComment("pk");
            entity.Property(e => e.Brief).HasComment("簡介");
            entity.Property(e => e.Btag).HasComment("標籤");
            entity.Property(e => e.BuilderId).HasComment("廠商ID");
            entity.Property(e => e.BuilderType).HasComment("建案類別");
            entity.Property(e => e.City).HasComment("縣市");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.Cover).HasComment("封面圖");
            entity.Property(e => e.Descr).HasComment("敘述");
            entity.Property(e => e.Email).HasComment("通知email");
            entity.Property(e => e.IsSend).HasComment("寄送次數");
            entity.Property(e => e.IsVideo)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否有影片(0:否 / 1:是");
            entity.Property(e => e.Istaging).HasComment("iStaging");
            entity.Property(e => e.Layout).HasComment("格局規劃	");
            entity.Property(e => e.LejuUrl).HasComment("樂居實價登錄網址");
            entity.Property(e => e.Name).HasComment("產品名稱");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.ReviewA).HasComment("交通評價");
            entity.Property(e => e.ReviewB).HasComment("生活機能評價");
            entity.Property(e => e.ReviewC).HasComment("建材公設評價");
            entity.Property(e => e.ReviewD).HasComment("總價評價");
            entity.Property(e => e.ReviewE).HasComment("空間坪數評價");
            entity.Property(e => e.SalesAssistantEmail).HasComment("負責業務助理郵件");
            entity.Property(e => e.SalesEmail).HasComment("負責業務郵件");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.Sort).HasComment("排序");
            entity.Property(e => e.TotalPrice).HasComment("總價	");
            entity.Property(e => e.Types).HasComment("類型");
            entity.Property(e => e.UnitPrice).HasComment("單價");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("更新時間");
            entity.Property(e => e.YtCover).HasComment("Youtube影片封面");
        });

        modelBuilder.Entity<CacheModel>(entity =>
        {
            entity.HasKey(e => e.CacheKey).HasName("PRIMARY");

            entity.Property(e => e.CacheKey).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Calculator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Bathroom).HasComment("格局-衛");
            entity.Property(e => e.Ceiling).HasComment("天花板類型");
            entity.Property(e => e.ChangeArea).HasComment("變動裝修");
            entity.Property(e => e.Compartment).HasComment("隔間材質");
            entity.Property(e => e.ContactStatus).HasComment("是否可聯繫客戶");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Email)
                .HasDefaultValueSql("'-'")
                .HasComment("電子郵件");
            entity.Property(e => e.Floor).HasComment("地板材質");
            entity.Property(e => e.HouseType).HasComment("房屋類型");
            entity.Property(e => e.HouseYear).HasComment("屋齡");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.IsFb)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否為FB帳號(Y:是 / N:否");
            entity.Property(e => e.Liveroom).HasComment("格局-廳");
            entity.Property(e => e.Loan).HasDefaultValueSql("b'0'");
            entity.Property(e => e.LoanStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("貸款意願");
            entity.Property(e => e.Location).HasComment("裝修地區");
            entity.Property(e => e.Message).HasComment("意見回饋");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("'-'")
                .HasComment("聯絡姓名");
            entity.Property(e => e.Phone)
                .HasDefaultValueSql("'-'")
                .HasComment("聯絡電話");
            entity.Property(e => e.Pin).HasComment("坪數");
            entity.Property(e => e.RentHouseStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否租屋(N:否 Y:是)");
            entity.Property(e => e.Room).HasComment("格局-房");
            entity.Property(e => e.SendMail)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出郵件(N : 否 / Y : 是)");
            entity.Property(e => e.SendTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("寄出時間");
            entity.Property(e => e.StorageStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("找收納達人意願N:否 Y:是) ");
            entity.Property(e => e.Total).HasComment("裝修預估總金額");
            entity.Property(e => e.WarehousingStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否尋找倉儲(N:否 Y:是)");
        });

        modelBuilder.Entity<CalculatorRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Area).HasComment("坪數");
            entity.Property(e => e.CaType).HasComment("裝修類型(輕裝修、全室裝修、局部裝修)");
            entity.Property(e => e.City).HasComment("所在縣市");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("電子郵件");
            entity.Property(e => e.HClass).HasComment("房屋類型(主要是全室裝修A/B版有，新版沒有)");
            entity.Property(e => e.MarketingConsent).HasComment("行銷同意(1:同意,0:不同意,2:無)");
            entity.Property(e => e.Name).HasComment("名字");
            entity.Property(e => e.Phone).HasComment("手機");
            entity.Property(e => e.SourceWeb).HasComment("(需求平台:全室A、全室B、官網)");
            entity.Property(e => e.UtmCampaign).HasComment("utm活動");
            entity.Property(e => e.UtmMedium).HasComment("utm媒介");
            entity.Property(e => e.UtmSource).HasComment("utm來源");
        });

        modelBuilder.Entity<CallinDatum>(entity =>
        {
            entity.HasKey(e => e.Seq).HasName("PRIMARY");

            entity.Property(e => e.SendMail).HasDefaultValueSql("'N'");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.ConfId).HasName("PRIMARY");

            entity.Property(e => e.ConfDesc).HasDefaultValueSql("''");
            entity.Property(e => e.ConfFormtype).HasDefaultValueSql("''");
            entity.Property(e => e.ConfName).HasDefaultValueSql("''");
            entity.Property(e => e.ConfTitle).HasDefaultValueSql("''");
            entity.Property(e => e.ConfValuetype).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Configcategory>(entity =>
        {
            entity.HasKey(e => e.ConfcatId).HasName("PRIMARY");

            entity.Property(e => e.ConfcatName).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Configoption>(entity =>
        {
            entity.HasKey(e => e.ConfopId).HasName("PRIMARY");

            entity.Property(e => e.ConfopName).HasDefaultValueSql("''");
            entity.Property(e => e.ConfopValue).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Company).HasComment("公司名稱");
            entity.Property(e => e.Content).HasComment("內容");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("電子郵件");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.IsFb)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否為FB帳號(Y:是 / N:否");
            entity.Property(e => e.Name).HasComment("姓名 / 公司名");
            entity.Property(e => e.Phone).HasComment("聯繫電話");
            entity.Property(e => e.Send)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(N:未寄出 / Y:已寄出)");
            entity.Property(e => e.SendTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("寄出時間");
            entity.Property(e => e.Subject).HasComment("主旨");
        });

        modelBuilder.Entity<DecoRecord>(entity =>
        {
            entity.HasKey(e => e.Bldsno).HasName("PRIMARY");

            entity.ToTable("deco_record", tb => tb.HasComment("室內裝修業登記資料"));

            entity.Property(e => e.Bldsno).HasComment("ID");
            entity.Property(e => e.Address).HasComment("公司地址	");
            entity.Property(e => e.Avatar).HasComment("Logo");
            entity.Property(e => e.AvatarXywd).HasComment("Logo尺寸位置");
            entity.Property(e => e.CompanyCeo).HasComment("負責人");
            entity.Property(e => e.CompanyName).HasComment("公司名稱");
            entity.Property(e => e.CompanyPeople).HasComment("專業人員人數");
            entity.Property(e => e.CompanyScope).HasComment("登記範圍");
            entity.Property(e => e.CreateDate).HasComment("設立日期");
            entity.Property(e => e.DataUpdateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ExpirtDate).HasComment("有效期限");
            entity.Property(e => e.HdesignerId).HasComment("FK:_hdesigner.hdesigner_id ");
            entity.Property(e => e.Note).HasComment("註記");
            entity.Property(e => e.ReissueDate).HasComment("換發日期");
            entity.Property(e => e.RenewPeriod).HasComment("換證期限	");
            entity.Property(e => e.UpdateDate).HasComment("最近異動日期");
        });

        modelBuilder.Entity<DecoRecordImg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Bldsno).HasComment("FK");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("建立時間");
            entity.Property(e => e.ImgPath).HasComment("圖片位置(檔名");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否審核通過(Y:是/N:否");
            entity.Property(e => e.Sort)
                .HasDefaultValueSql("'1'")
                .HasComment("順序");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("更新時間");
        });

        modelBuilder.Entity<DecoRecordPerson>(entity =>
        {
            entity.HasKey(e => e.Seq).HasName("PRIMARY");

            entity.ToTable("deco_record_person", tb => tb.HasComment("聘用專業技術人員清冊"));

            entity.Property(e => e.EmploymentDate).HasComment("到職日期");
            entity.Property(e => e.Name).HasComment("姓名");
            entity.Property(e => e.RecordNumber).HasComment("登記證書字號");
            entity.Property(e => e.Registration).HasComment("登記資格");
        });

        modelBuilder.Entity<DecoRequest>(entity =>
        {
            entity.HasKey(e => e.Seq).HasName("PRIMARY");

            entity.ToTable("deco_request", tb => tb.HasComment("軟裝需求單"));

            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Budget).HasComment("預算");
            entity.Property(e => e.DecoPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("已收費用");
            entity.Property(e => e.DecoSetPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("丈量費");
            entity.Property(e => e.Email).HasComment("電子郵件	");
            entity.Property(e => e.FirstContact)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("第一次來電日期");
            entity.Property(e => e.Functions).HasComment("需求功能");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("'-'")
                .HasComment("GUID");
            entity.Property(e => e.HouseAllLv).HasComment("大樓高幾樓");
            entity.Property(e => e.HouseHight).HasComment("挑高高度");
            entity.Property(e => e.HouseInLv).HasComment("要裝修第幾樓");
            entity.Property(e => e.HouseMode).HasComment("房屋型態");
            entity.Property(e => e.HouseType).HasComment("房屋類型");
            entity.Property(e => e.HouseYears).HasComment("屋齡");
            entity.Property(e => e.HowToKnow).HasComment("何處得知軟裝");
            entity.Property(e => e.IsAgree).HasComment("同意接受案件竣工後將進行紀錄，並作為媒體宣傳的露出使用");
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否刪除(Y:是/N:否");
            entity.Property(e => e.Layout).HasComment("需求格局");
            entity.Property(e => e.LineId).HasComment("LINE_ID");
            entity.Property(e => e.Loan).HasDefaultValueSql("b'0'");
            entity.Property(e => e.MeetingDate).HasComment("約定辦公室面訪時間");
            entity.Property(e => e.Name).HasComment("姓名");
            entity.Property(e => e.NeedRequest).HasComment("您需要進行");
            entity.Property(e => e.NoteText).HasComment("備註欄位需求");
            entity.Property(e => e.NowLayout).HasComment("目前格局(_房,_廳,_衛)");
            entity.Property(e => e.Pattern).HasComment("成人小孩老人");
            entity.Property(e => e.PaymentStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("付款狀態");
            entity.Property(e => e.PaymentTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("付款時間");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.PhoneRepeat)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("重複資料(裝修需求單 ");
            entity.Property(e => e.Pin).HasComment("坪數");
            entity.Property(e => e.ProposalPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("提案費");
            entity.Property(e => e.SendDatetime).HasComment("寄出時間");
            entity.Property(e => e.SendStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(Y:是 / N:否	");
            entity.Property(e => e.SetDate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("提醒日期");
            entity.Property(e => e.Sex)
                .HasDefaultValueSql("'女性'")
                .IsFixedLength()
                .HasComment("性別");
            entity.Property(e => e.Source)
                .HasDefaultValueSql("'經紀人部門'")
                .HasComment("資料來源(經紀人部門,自動來電,前台表單,講座)");
            entity.Property(e => e.Style).HasComment("風格需求");
            entity.Property(e => e.Time).HasComment("裝修日期");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("'0.待辦'")
                .HasComment("分類(待辦,結案,進行中)");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("更新時間");
            entity.Property(e => e.UtmCampaign).HasComment("utm活動");
            entity.Property(e => e.UtmMedium).HasComment("utm媒介");
            entity.Property(e => e.UtmSource)
                .HasDefaultValueSql("'-'")
                .HasComment("utm_source");
            entity.Property(e => e.WantDecoBudget)
                .HasDefaultValueSql("'無'")
                .HasComment("裝修預算");
            entity.Property(e => e.WantDecoMatters)
                .HasDefaultValueSql("'無'")
                .HasComment("裝修事項");
            entity.Property(e => e.WantDecoTime)
                .HasDefaultValueSql("'無'")
                .HasComment("預計裝修時間");
        });

        modelBuilder.Entity<DecoRequestFile>(entity =>
        {
            entity.HasKey(e => e.DecoFileId).HasName("PRIMARY");
        });

        modelBuilder.Entity<DecoqueryHistory>(entity =>
        {
            entity.HasKey(e => e.SearchId).HasName("PRIMARY");

            entity.ToTable("decoquery_history", tb => tb.HasComment("關鍵字搜尋紀錄"));

            entity.Property(e => e.SearchId).HasComment("id");
            entity.Property(e => e.DateAdded).HasComment("日期");
            entity.Property(e => e.Keyword).HasComment("關鍵字");
            entity.Property(e => e.TodayCount).HasComment("次數");
        });

        modelBuilder.Entity<Decoration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Area).HasComment("所在地區");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("電子郵件");
            entity.Property(e => e.Name).HasComment("聯絡姓名");
            entity.Property(e => e.Phone).HasComment("聯絡電話");
            entity.Property(e => e.Pin).HasComment("房屋實際坪數");
            entity.Property(e => e.Type).HasComment("您目前房屋類型");
        });

        modelBuilder.Entity<DesignerBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.BranchOffice).HasDefaultValueSql("'show'");
            entity.Property(e => e.BranchOfficeTitle).HasDefaultValueSql("'none'");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.DesignerId).HasComment("設計師id");
            entity.Property(e => e.Email).HasComment("信箱");
            entity.Property(e => e.Fax).HasComment("傳真");
            entity.Property(e => e.Line).HasComment("line帳號");
            entity.Property(e => e.Tel).HasComment("電話");
            entity.Property(e => e.Title)
                .HasDefaultValueSql("'分公司地址'")
                .HasComment("公司抬頭");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<DesignerKeywordSearch>(entity =>
        {
            entity.Property(e => e.Count)
                .HasDefaultValueSql("'0'")
                .HasComment("次數");
            entity.Property(e => e.Name).HasComment("名稱");
        });

        modelBuilder.Entity<EdmLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Content).HasComment("信件內容");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.IsSend).HasComment("是否已寄送");
            entity.Property(e => e.IsSendTime).HasComment("更新時間");
            entity.Property(e => e.Subject).HasComment("信件主旨");
            entity.Property(e => e.TestMail).HasComment("測試收信人");
            entity.Property(e => e.Url).HasComment("信件URL");
            entity.Property(e => e.UserCount).HasComment("寄送時間");
            entity.Property(e => e.UserGroup).HasComment("收件群組");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventsId).HasName("PRIMARY");

            entity.ToTable("events", tb => tb.HasComment("活動表單"));

            entity.Property(e => e.Uid).HasComment("會員id");
        });

        modelBuilder.Entity<ExecuteDetail>(entity =>
        {
            entity.HasKey(e => e.ExdId).HasName("PRIMARY");

            entity.Property(e => e.ExdId).HasComment("PK");
            entity.Property(e => e.AlertDate1).HasComment("第一次提醒日期");
            entity.Property(e => e.AlertDate2).HasComment("第二次提醒日期");
            entity.Property(e => e.AlertDays).HasComment("幾天前發送通知");
            entity.Property(e => e.CompleteMan).HasComment("完成人(郵件)");
            entity.Property(e => e.CompleteTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("完成時間");
            entity.Property(e => e.CreateNum)
                .HasDefaultValueSql("'1'")
                .HasComment("建立幾個項目");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("建立時間");
            entity.Property(e => e.ExecuteMan).HasComment("執行部門(郵件)");
            entity.Property(e => e.ExfId).HasComment("FK：execute_form");
            entity.Property(e => e.IsAllow)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否同意執行");
            entity.Property(e => e.IsComplete)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否完成 (Y:是 / N:否/ D:轉約");
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否刪除(Y:是 / N:否)");
            entity.Property(e => e.IsSend)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出通知(N：否 / Y：是)");
            entity.Property(e => e.Lv1).HasComment("大項目");
            entity.Property(e => e.Lv2).HasComment("小項目");
            entity.Property(e => e.Note).HasComment("備註");
            entity.Property(e => e.Price).HasComment("合約金額(未稅)");
            entity.Property(e => e.SendTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("寄出時間");
            entity.Property(e => e.SetDate).HasComment("預計排程日期");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("更新時間");
        });

        modelBuilder.Entity<ExecuteFile>(entity =>
        {
            entity.HasKey(e => e.ExfFileId).HasName("PRIMARY");
        });

        modelBuilder.Entity<ExecuteForm>(entity =>
        {
            entity.HasKey(e => e.ExfId).HasName("PRIMARY");

            entity.Property(e => e.ExfId).HasComment("PK");
            entity.Property(e => e.AllowFinance).HasComment("財務同意");
            entity.Property(e => e.AllowFinanceTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("財務同意時間");
            entity.Property(e => e.AllowMan).HasComment("主管同意");
            entity.Property(e => e.AllowSales).HasComment("業務同意");
            entity.Property(e => e.AllowSalesTime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("業務同意時間");
            entity.Property(e => e.AllowTime).HasComment("同意執行時間");
            entity.Property(e => e.Company).HasComment("設計公司");
            entity.Property(e => e.ContractPerson).HasComment("聯絡人");
            entity.Property(e => e.ContractTime).HasComment("到期日");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Creator).HasComment("建立者");
            entity.Property(e => e.Designer).HasComment("設計師");
            entity.Property(e => e.DetailStatus)
                .HasDefaultValueSql("'1'")
                .HasComment("合約樣板");
            entity.Property(e => e.Edate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("下架日期");
            entity.Property(e => e.FbPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("FB投放費用");
            entity.Property(e => e.HostPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("主持費用");
            entity.Property(e => e.IsClose)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否結案(N:否 / Y:是 / T:未上線");
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否刪除(Y:是 / N:否)");
            entity.Property(e => e.LastUpdate).HasComment("最後更新者");
            entity.Property(e => e.Mobile).HasComment("手機");
            entity.Property(e => e.Note).HasComment("備註說明");
            entity.Property(e => e.Num).HasComment("合約編號");
            entity.Property(e => e.PhotoOutsidePrice)
                .HasDefaultValueSql("'0'")
                .HasComment("攝影外拍費用");
            entity.Property(e => e.PhotoTransPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("攝影交通費");
            entity.Property(e => e.Price).HasComment("合約金額(未稅)");
            entity.Property(e => e.Quota).HasComment("額度(已使用數/總額度數)");
            entity.Property(e => e.SalesDept).HasComment("部門別");
            entity.Property(e => e.SalesMan).HasComment("接案業務");
            entity.Property(e => e.Sdate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("上架日期");
            entity.Property(e => e.TaxIncludedPrice).HasComment("合約金額(含稅");
            entity.Property(e => e.Telete).HasComment("電話");
            entity.Property(e => e.TransferNum).HasComment("轉約前號碼(用,隔開)");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
            entity.Property(e => e.YtPrice)
                .HasDefaultValueSql("'0'")
                .HasComment("YT投放費用");
        });

        modelBuilder.Entity<ExecuteItem>(entity =>
        {
            entity.HasKey(e => e.ExecuteItemsId).HasName("PRIMARY");

            entity.ToTable("execute_items", tb => tb.HasComment("執行項目表"));

            entity.Property(e => e.ContractName).HasComment("合約名稱");
            entity.Property(e => e.ContractType).HasComment("分類");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("執行項");
            entity.Property(e => e.IsDel).HasComment("是否刪除(0:否 / 1:是");
            entity.Property(e => e.Name).HasComment("大項目");
        });

        modelBuilder.Entity<ExecuteSignOff>(entity =>
        {
            entity.HasKey(e => e.EsoId).HasName("PRIMARY");

            entity.ToTable("execute_sign_off", tb => tb.HasComment("簽核資料"));

            entity.Property(e => e.EsoId).HasComment("PK");
            entity.Property(e => e.Allow)
                .HasDefaultValueSql("'-'")
                .IsFixedLength()
                .HasComment("同意");
            entity.Property(e => e.AllowDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("同意時間");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("時間");
            entity.Property(e => e.Deny)
                .HasDefaultValueSql("'-'")
                .IsFixedLength()
                .HasComment("拒絕");
            entity.Property(e => e.DenyDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("拒絕時間");
            entity.Property(e => e.FromPk).HasComment("PK");
            entity.Property(e => e.FromTable).HasComment("資料表");
            entity.Property(e => e.Identity).HasComment("覆核對象");
            entity.Property(e => e.Note).HasComment("備註");
            entity.Property(e => e.Review).HasComment("覆核人");
            entity.Property(e => e.Sort).HasComment("審核排序");
        });

        modelBuilder.Entity<Forget>(entity =>
        {
            entity.HasKey(e => e.ForgetId).HasName("PRIMARY");

            entity.Property(e => e.ForgetId).HasComment("pk");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("email");
            entity.Property(e => e.EmailGuid).HasComment("eamil識別碼");
            entity.Property(e => e.Guid).HasComment("會員全球唯一碼");
            entity.Property(e => e.Status)
                .IsFixedLength()
                .HasComment("狀態(0:未使用 1:已使用)");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<ForumArticle>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PRIMARY");

            entity.ToTable("forum_article", tb => tb.HasComment("討論區文章"));

            entity.Property(e => e.ArticleId).HasComment("文章編號");
            entity.Property(e => e.BadCount).HasComment("不好的數量");
            entity.Property(e => e.Category).HasComment("文章分類");
            entity.Property(e => e.Content).HasComment("內容");
            entity.Property(e => e.DateAdded).HasComment("建立時間");
            entity.Property(e => e.DateModified).HasComment("修改時間");
            entity.Property(e => e.Description).HasComment("簡介");
            entity.Property(e => e.GoodCount).HasComment("好的數量");
            entity.Property(e => e.Guid).HasComment("GUID");
            entity.Property(e => e.IsDel).HasComment("刪除 0:否 1:是");
            entity.Property(e => e.IsHidden).HasComment("是否自己隱藏(0:否 / 1:是");
            entity.Property(e => e.IsTop).HasComment("置頂");
            entity.Property(e => e.ReadCount).HasComment("讀取次數");
            entity.Property(e => e.ReplyCount).HasComment("回覆數量");
            entity.Property(e => e.ReplyRead)
                .HasDefaultValueSql("'1'")
                .HasComment("0:未讀 1:已讀");
            entity.Property(e => e.Title).HasComment("標題");
            entity.Property(e => e.Uid).HasComment("發文使用者");
        });

        modelBuilder.Entity<ForumArticleReply>(entity =>
        {
            entity.HasKey(e => e.ArticleReplyId).HasName("PRIMARY");

            entity.Property(e => e.DateAdded).HasComment("建立時間");
            entity.Property(e => e.DateModified).HasComment("修改時間");
            entity.Property(e => e.IsDel).HasComment("是否刪除(0:否 / 1:是)");
            entity.Property(e => e.IsHidden).HasComment("是否自己隱藏(0:否 / 1:是");
            entity.Property(e => e.ReplyBadCount).HasComment("回覆不好的數量");
            entity.Property(e => e.ReplyContent).HasComment("回覆內容");
            entity.Property(e => e.ReplyGoodCount).HasComment("回覆好的數量");
            entity.Property(e => e.Uid).HasComment("回覆會員");
        });

        modelBuilder.Entity<ForumArticleReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PRIMARY");

            entity.ToTable("forum_article_report", tb => tb.HasComment("檢舉文章"));

            entity.Property(e => e.DataId).HasComment("文章或回覆文章id	");
            entity.Property(e => e.DateAdded).HasComment("建立時間");
            entity.Property(e => e.Ip).HasComment("ip");
            entity.Property(e => e.ReportType).HasComment("文章類型 0:本文 1:回覆	");
            entity.Property(e => e.SendDatetime).HasComment("寄出時間");
            entity.Property(e => e.SendEmail)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否寄出(0 : 未寄出 / 1 : 已寄出");
            entity.Property(e => e.Uid).HasComment("使用者編號	");
        });

        modelBuilder.Entity<ForumArticleReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.ToTable("forum_article_review", tb => tb.HasComment("討論區文章評論"));

            entity.Property(e => e.ReviewId).HasComment("評論ID");
            entity.Property(e => e.DataId).HasComment("文章或回覆文章id");
            entity.Property(e => e.DateModified).HasComment("修改時間");
            entity.Property(e => e.ReviewStatus).HasComment("評論狀態 0:無評論 1:讚 2::不讚");
            entity.Property(e => e.ReviewType).HasComment("評論文章類型 0:本文 1:回覆");
            entity.Property(e => e.Uid).HasComment("使用者編號");
        });

        modelBuilder.Entity<ForumTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.ArticleId).HasComment("FK:forum_article.artcle_id");
            entity.Property(e => e.CheckDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("讀取檢驗時間");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Uid).HasComment("FK:_users.uid");
        });

        modelBuilder.Entity<Forward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Content).HasComment("內文");
            entity.Property(e => e.InsTime).HasComment("建立時間");
            entity.Property(e => e.MessageId).HasComment("MessageID");
            entity.Property(e => e.Recipient).HasComment("收件者");
            entity.Property(e => e.SendStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(N:否 / Y:是)");
            entity.Property(e => e.SendTime).HasComment("寄出時間");
            entity.Property(e => e.Sender).HasComment("寄件者");
            entity.Property(e => e.Subject).HasComment("表題");
            entity.Property(e => e.Uid).HasComment("FK:_users.uid");
            entity.Property(e => e.Url).HasComment("網址");
        });

        modelBuilder.Entity<GoStorage>(entity =>
        {
            entity.HasKey(e => e.Seq).HasName("PRIMARY");

            entity.ToTable("go_storage", tb => tb.HasComment("go收納"));

            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Budget).HasComment("預算");
            entity.Property(e => e.Email).HasComment("電子郵件	");
            entity.Property(e => e.IsAgree).HasComment("同意接受案件竣工後將進行紀錄，並作為媒體宣傳的露出使用");
            entity.Property(e => e.Layout).HasComment("需求格局");
            entity.Property(e => e.Name).HasComment("姓名");
            entity.Property(e => e.Pattern).HasComment("成人小孩老人");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Pin).HasComment("坪數");
            entity.Property(e => e.SendDatetime).HasComment("寄出時間");
            entity.Property(e => e.SendStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(Y:是 / N:否	");
            entity.Property(e => e.Time).HasComment("裝修日期");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("PRIMARY");

            entity.Property(e => e.GroupType).HasDefaultValueSql("''");
            entity.Property(e => e.Name).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(e => e.GpermId).HasName("PRIMARY");

            entity.HasIndex(e => new { e.GpermModid, e.GpermName }, "gperm_modid").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 10 });

            entity.Property(e => e.GpermItemid).HasDefaultValueSql("'0'");
            entity.Property(e => e.GpermModid).HasDefaultValueSql("'0'");
            entity.Property(e => e.GpermName).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<GroupsUsersLink>(entity =>
        {
            entity.HasKey(e => e.Linkid).HasName("PRIMARY");

            entity.Property(e => e.Uid).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<HLineRss>(entity =>
        {
            entity.HasKey(e => e.DateString).HasName("PRIMARY");

            entity.Property(e => e.DateString).HasComment("日期");
            entity.Property(e => e.StrCaseId).HasComment("個案ID");
            entity.Property(e => e.StrId).HasComment("專欄ID");
            entity.Property(e => e.UpdateTimeUnix).HasComment("更新時間");
        });

        modelBuilder.Entity<Had>(entity =>
        {
            entity.HasKey(e => e.Adid).HasName("PRIMARY");

            entity.Property(e => e.Adid).HasComment("pk");
            entity.Property(e => e.Addesc).HasComment("描述");
            entity.Property(e => e.Adhref).HasComment("廣告連結");
            entity.Property(e => e.AdhrefR).HasComment("廣告連結(右邊");
            entity.Property(e => e.Adlogo).HasComment("LOGO");
            entity.Property(e => e.AdlogoMobile).HasComment("手機版圖片");
            entity.Property(e => e.AdlogoMobileWebp).HasComment("手機版webp");
            entity.Property(e => e.AdlogoWebp).HasComment("桌機版webp");
            entity.Property(e => e.Adlongdesc).HasComment("長描述");
            entity.Property(e => e.Adtype).HasComment("廣告類型");
            entity.Property(e => e.AltUse)
                .HasDefaultValueSql("'幸福空間'")
                .HasComment("alt使用預設為幸福空間");
            entity.Property(e => e.BuilderProductId).HasComment("建案ID	");
            entity.Property(e => e.ClickCounter).HasComment("點擊數");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.EndTime).HasComment("結束時間");
            entity.Property(e => e.HbrandId).HasComment("廠商ID	");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.IndexChar1).HasComment("首頁大廣告下方文字第一段");
            entity.Property(e => e.IndexChar21).HasComment("首頁大廣告下方文字第二段第一節");
            entity.Property(e => e.IndexChar22).HasComment("首頁大廣告下方文字第二段第二節");
            entity.Property(e => e.IndexChar23).HasComment("首頁大廣告下方文字第二段第三節");
            entity.Property(e => e.IsSend)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出通知(N:否 / Y:是");
            entity.Property(e => e.Keyword).HasComment("關鍵字");
            entity.Property(e => e.LogoIcon).HasComment("Logo圖示");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.SendDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("寄送時間");
            entity.Property(e => e.StartTime).HasComment("開始時間");
            entity.Property(e => e.Tabname).HasComment("tab名稱(已經從頁面拿掉了)");
            entity.Property(e => e.WaterMark).HasComment("大banner浮水logo ");
        });

        modelBuilder.Entity<Haward>(entity =>
        {
            entity.HasKey(e => e.HawardsId).HasName("PRIMARY");

            entity.Property(e => e.HawardsId).HasComment("pk");
            entity.Property(e => e.AwardsName).HasComment("獎項名稱");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.Logo).HasComment("獎項LOGO");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
        });

        modelBuilder.Entity<Hbrand>(entity =>
        {
            entity.HasKey(e => e.HbrandId).HasName("PRIMARY");

            entity.Property(e => e.HbrandId).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.BackgroundMobile).HasComment("手機背景圖");
            entity.Property(e => e.Border).HasComment("排序");
            entity.Property(e => e.Bspace).HasComment("已從頁面移除");
            entity.Property(e => e.Bstype).HasComment("已從頁面移除");
            entity.Property(e => e.Btype).HasComment("產品類別");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.ContactLink).HasComment("與廠商聯繫連結");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("品牌描述");
            entity.Property(e => e.Email).HasComment("廠商EMAIL");
            entity.Property(e => e.Fbpageurl).HasComment("Facebook page URL");
            entity.Property(e => e.Gchoice).HasComment("幸福空間推薦");
            entity.Property(e => e.History).HasComment("獲獎紀錄");
            entity.Property(e => e.HvideoId).HasComment("影片ID");
            entity.Property(e => e.Imported).HasComment("進口品牌");
            entity.Property(e => e.Intro).HasComment("品牌介紹");
            entity.Property(e => e.IsSend).HasComment("已寄送次數");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Logo2).HasComment("logo2");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關閉 1:開啟)");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.ProductId).HasComment("產品封面圖id");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.SalesEmail).HasComment("負責業務EMAIL");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.SubCompany).HasComment("分公司");
            entity.Property(e => e.Title).HasComment("廠商名稱");
            entity.Property(e => e.Vr360Id).HasComment("vr306_ID");
            entity.Property(e => e.Website).HasComment("網站");
        });

        modelBuilder.Entity<HbrandPage>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");
        });

        modelBuilder.Entity<Hcase>(entity =>
        {
            entity.HasKey(e => e.HcaseId).HasName("PRIMARY");

            entity.Property(e => e.HcaseId).HasComment("pk");
            entity.Property(e => e.Area).HasComment("房屋坪數");
            entity.Property(e => e.AreaDesc).HasComment("房屋坪數補充說明");
            entity.Property(e => e.AutoCountFee).HasComment("自動計算裝潢費用(0: 否 / 1:是)");
            entity.Property(e => e.Caption).HasComment("個案名稱");
            entity.Property(e => e.CaseTop).HasDefaultValueSql("'N'");
            entity.Property(e => e.Condition).HasComment("房屋狀況");
            entity.Property(e => e.Corder).HasComment("排序");
            entity.Property(e => e.Cover).HasComment("封面圖");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Fee).HasComment("裝潢費用");
            entity.Property(e => e.Feedesc).HasComment("裝潢費用補充說明");
            entity.Property(e => e.HdesignerId).HasComment("設計師id");
            entity.Property(e => e.IsSend).HasComment("已寄送次數");
            entity.Property(e => e.Istaging).HasComment("iStage");
            entity.Property(e => e.Layout).HasComment("空間格局");
            entity.Property(e => e.Level)
                .HasDefaultValueSql("'1'")
                .HasComment("預算計算的加成比例");
            entity.Property(e => e.Location).HasComment("房屋位置");
            entity.Property(e => e.LongDesc).HasComment("長說明");
            entity.Property(e => e.Materials).HasComment("主要建材");
            entity.Property(e => e.Member).HasComment("居住成員");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Provider).HasComment("圖片提供");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sdate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("上架日期(2020-02-12)");
            entity.Property(e => e.SdateOrder).HasDefaultValueSql("'0'");
            entity.Property(e => e.ShortDesc).HasComment("短說明");
            entity.Property(e => e.SizeByte).HasComment("圖片尺寸");
            entity.Property(e => e.Style).HasComment("設計風格");
            entity.Property(e => e.Style2).HasComment("自訂的設計風格");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.Type).HasComment("房屋類型");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.Viewed).HasComment("觀看數");
            entity.Property(e => e.Vr360Id).HasComment("VR360 ID");
        });

        modelBuilder.Entity<Hcase0511>(entity =>
        {
            entity.HasKey(e => e.HcaseId).HasName("PRIMARY");

            entity.Property(e => e.HcaseId).HasComment("pk");
            entity.Property(e => e.Area).HasComment("房屋坪數");
            entity.Property(e => e.AreaDesc).HasComment("房屋坪數補充說明");
            entity.Property(e => e.AutoCountFee).HasComment("自動計算裝潢費用(0: 否 / 1:是)");
            entity.Property(e => e.Caption).HasComment("個案名稱");
            entity.Property(e => e.Condition).HasComment("房屋狀況");
            entity.Property(e => e.Corder).HasComment("排序");
            entity.Property(e => e.Cover).HasComment("封面圖");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Fee).HasComment("裝潢費用");
            entity.Property(e => e.Feedesc).HasComment("裝潢費用補充說明");
            entity.Property(e => e.HdesignerId).HasComment("設計師id");
            entity.Property(e => e.IsSend).HasComment("已寄送次數");
            entity.Property(e => e.Istaging).HasComment("iStage");
            entity.Property(e => e.Layout).HasComment("空間格局");
            entity.Property(e => e.Level)
                .HasDefaultValueSql("'1'")
                .HasComment("預算計算的加成比例");
            entity.Property(e => e.Location).HasComment("房屋位置");
            entity.Property(e => e.LongDesc).HasComment("長說明");
            entity.Property(e => e.Materials).HasComment("主要建材");
            entity.Property(e => e.Member).HasComment("居住成員");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Provider).HasComment("圖片提供");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sdate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("上架日期(2020-02-12)");
            entity.Property(e => e.ShortDesc).HasComment("短說明");
            entity.Property(e => e.SizeByte).HasComment("圖片尺寸");
            entity.Property(e => e.Style).HasComment("設計風格");
            entity.Property(e => e.Style2).HasComment("自訂的設計風格");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.Type).HasComment("房屋類型");
            entity.Property(e => e.Viewed).HasComment("觀看數");
            entity.Property(e => e.Vr360Id).HasComment("VR360 ID");
        });

        modelBuilder.Entity<HcaseImg>(entity =>
        {
            entity.HasKey(e => e.HcaseImgId).HasName("PRIMARY");

            entity.Property(e => e.HcaseImgId).HasComment("pk");
            entity.Property(e => e.ChangeName)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("換圖處理");
            entity.Property(e => e.Desc).HasComment("描述");
            entity.Property(e => e.H).HasComment("高");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.IsCover).HasComment("設為封面(0:否 1:是)");
            entity.Property(e => e.IsFlat).HasComment("是否為平面圖");
            entity.Property(e => e.IsHint).HasComment("是否為3D示意圖");
            entity.Property(e => e.Name).HasComment("檔案名稱");
            entity.Property(e => e.Order).HasComment("排序號");
            entity.Property(e => e.SizeByte).HasComment("檔案大小");
            entity.Property(e => e.Tag1).HasComment("Tag");
            entity.Property(e => e.Tag2).HasComment("Tag : 家具、燈飾、軟裝");
            entity.Property(e => e.Tag3).HasComment("Tag : 顏色");
            entity.Property(e => e.Tag4).HasComment("Tag : 建材、家電與設備 ");
            entity.Property(e => e.Tag5).HasComment("Tag : 其他主題");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.TagMan).HasComment("Tag編寫者");
            entity.Property(e => e.Title).HasComment("標題");
            entity.Property(e => e.Viewed).HasComment("觀看次數");
            entity.Property(e => e.W).HasComment("寬");
        });

        modelBuilder.Entity<HcaseImg0511>(entity =>
        {
            entity.HasKey(e => e.HcaseImgId).HasName("PRIMARY");

            entity.Property(e => e.HcaseImgId).HasComment("pk");
            entity.Property(e => e.Desc).HasComment("描述");
            entity.Property(e => e.H).HasComment("高");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.IsCover).HasComment("設為封面(0:否 1:是)");
            entity.Property(e => e.IsFlat).HasComment("是否為平面圖");
            entity.Property(e => e.IsHint).HasComment("是否為3D示意圖");
            entity.Property(e => e.Name).HasComment("檔案名稱");
            entity.Property(e => e.Order).HasComment("排序號");
            entity.Property(e => e.SizeByte).HasComment("檔案大小");
            entity.Property(e => e.Tag1).HasComment("Tag");
            entity.Property(e => e.Tag2).HasComment("Tag : 家具、燈飾、軟裝");
            entity.Property(e => e.Tag3).HasComment("Tag : 顏色");
            entity.Property(e => e.Tag4).HasComment("Tag : 建材、家電與設備 ");
            entity.Property(e => e.Tag5).HasComment("Tag : 其他主題");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.TagMan).HasComment("Tag編寫者");
            entity.Property(e => e.Title).HasComment("標題");
            entity.Property(e => e.Viewed).HasComment("觀看次數");
            entity.Property(e => e.W).HasComment("寬");
        });

        modelBuilder.Entity<Hclick>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Hcolumn>(entity =>
        {
            entity.HasKey(e => e.HcolumnId).HasName("PRIMARY");

            entity.HasIndex(e => e.Ctag, "ctag").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.HcolumnId).HasComment("pk");
            entity.Property(e => e.Bid).HasComment("廠商ID");
            entity.Property(e => e.BuilderProductId).HasComment("建案id");
            entity.Property(e => e.Cdesc).HasComment("專欄敘述");
            entity.Property(e => e.Clogo).HasComment("專欄logo");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.CshortTitle).HasComment("短專欄名稱");
            entity.Property(e => e.Ctag).HasComment("標籤");
            entity.Property(e => e.Ctitle).HasComment("專欄名稱");
            entity.Property(e => e.Ctype).HasComment("專欄類別");
            entity.Property(e => e.CtypeSub).HasComment("專欄類別_子項");
            entity.Property(e => e.ExtendStr).HasComment("圖文提供");
            entity.Property(e => e.HdesignerIds).HasComment("設計師ID");
            entity.Property(e => e.IsSend).HasComment("寄送次數");
            entity.Property(e => e.JsonBid).HasComment("json:廠商ID");
            entity.Property(e => e.JsonDid).HasComment("json:設計師ID");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.PageContent).HasComment("自由內容");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sdate)
                .HasDefaultValueSql("'0000-00-00'")
                .HasComment("上架日期(2020-02-12))");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.Viewed).HasComment("觀看數");
        });

        modelBuilder.Entity<HcolumnImg>(entity =>
        {
            entity.HasKey(e => e.HcolumnImgId).HasName("PRIMARY");
        });

        modelBuilder.Entity<HcolumnPage>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.Property(e => e.Sn).HasComment("pk");
            entity.Property(e => e.Cphtml).HasComment("自由內容html");
            entity.Property(e => e.Cporder).HasComment("排序");
            entity.Property(e => e.HcolumnId).HasComment("專欄ID");
        });

        modelBuilder.Entity<HcomparisonTbl>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");
        });

        modelBuilder.Entity<Hcontest>(entity =>
        {
            entity.HasKey(e => e.ContestId).HasName("PRIMARY");

            entity.Property(e => e.ContestId).HasComment("pk");
            entity.Property(e => e.An).HasComment("末五碼");
            entity.Property(e => e.Applytime).HasDefaultValueSql("'0000-00-00 00:00:00'");
            entity.Property(e => e.C1).HasComment("組別2");
            entity.Property(e => e.C10).HasComment("作品描述");
            entity.Property(e => e.C11).HasComment("作品描述2");
            entity.Property(e => e.C12).HasComment("作品描述3");
            entity.Property(e => e.C2).HasComment("報名者");
            entity.Property(e => e.C3).HasComment("公司/學校");
            entity.Property(e => e.C5).HasComment("電話");
            entity.Property(e => e.C6).HasComment("手機");
            entity.Property(e => e.C7).HasComment("地址");
            entity.Property(e => e.C8).HasComment("email");
            entity.Property(e => e.C9).HasComment("作品");
            entity.Property(e => e.ClassType).HasComment("組別1");
            entity.Property(e => e.Finalist).HasComment("入圍");
            entity.Property(e => e.Uid).HasComment("ID");
            entity.Property(e => e.Year).HasComment("年份");
        });

        modelBuilder.Entity<HcontestForum>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.Property(e => e.Applytime).HasDefaultValueSql("'0000-00-00 00:00:00'");
        });

        modelBuilder.Entity<HcontestVote>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");
        });

        modelBuilder.Entity<Hdesigner>(entity =>
        {
            entity.HasKey(e => e.HdesignerId).HasName("PRIMARY");

            entity.Property(e => e.HdesignerId).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Area).HasComment("接案坪數");
            entity.Property(e => e.Awards).HasComment("獲獎紀錄");
            entity.Property(e => e.AwardsLogo).HasComment("獎項logo");
            entity.Property(e => e.AwardsName).HasComment("獎項名稱");
            entity.Property(e => e.Background).HasComment("設計師背景圖");
            entity.Property(e => e.BackgroundMobile).HasComment("手機板背景圖");
            entity.Property(e => e.Blog).HasComment("其他網址連結");
            entity.Property(e => e.Budget).HasComment("接案預算");
            entity.Property(e => e.Career).HasComment("相關經歷");
            entity.Property(e => e.Charge).HasComment("收費方式");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CoordinateX).HasComment("座標X");
            entity.Property(e => e.CoordinateY).HasComment("座標Y");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Description).HasComment("header.description");
            entity.Property(e => e.DesignerMail).HasComment("通知設計師的email");
            entity.Property(e => e.Dorder)
                .HasDefaultValueSql("'99999'")
                .HasComment("排序");
            entity.Property(e => e.Emering)
                .HasDefaultValueSql("'0'")
                .HasComment("1:新銳設計師");
            entity.Property(e => e.Fax).HasComment("傳真");
            entity.Property(e => e.Fbpageurl).HasComment("FB page URL");
            entity.Property(e => e.Guarantee).HasComment("幸福經紀人");
            entity.Property(e => e.Idea).HasComment("設計理念");
            entity.Property(e => e.ImgPath).HasComment("頭像");
            entity.Property(e => e.IsSend).HasComment("寄送數目");
            entity.Property(e => e.JsonLd).HasComment("JSON-LD內容");
            entity.Property(e => e.License).HasComment("相關證照");
            entity.Property(e => e.LineLink).HasComment("LINE URL");
            entity.Property(e => e.Location).HasComment("設計師所在區域");
            entity.Property(e => e.Mail).HasComment("E-mail");
            entity.Property(e => e.MaxBudget).HasComment("預算最大值");
            entity.Property(e => e.MetaDescription).HasComment("Meta內容");
            entity.Property(e => e.MinBudget).HasComment("預算最小值");
            entity.Property(e => e.MobileOrder)
                .HasDefaultValueSql("'99999'")
                .HasComment("手機板排序");
            entity.Property(e => e.Name).HasComment("設計師名稱");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.OrderComputer)
                .HasDefaultValueSql("'day'")
                .HasComment("電腦版  day:用最新預設  favor:用人氣預設");
            entity.Property(e => e.OrderMb)
                .HasDefaultValueSql("'day'")
                .HasComment("手機版   day:用最新預設   favor:用人氣預設");
            entity.Property(e => e.Payment).HasComment("付費方式");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Position).HasComment("品牌定位");
            entity.Property(e => e.Premium)
                .HasDefaultValueSql("'0'")
                .HasComment("1:優質設計師");
            entity.Property(e => e.Region).HasComment("接案區域");
            entity.Property(e => e.SalesMail).HasComment("業務的email");
            entity.Property(e => e.SearchKeywords).HasComment("站內搜尋關鍵字");
            entity.Property(e => e.SearchKeywordsAuto).HasComment("站內搜尋關鍵字(電腦自動)");
            entity.Property(e => e.Seo).HasComment("SEO內容");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.Special).HasComment("特殊接案");
            entity.Property(e => e.Style).HasComment("接案風格");
            entity.Property(e => e.Taxid).HasComment("公司統編");
            entity.Property(e => e.Title).HasComment("公司抬頭");
            entity.Property(e => e.Top)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("置頂(N: 否 / Y:是");
            entity.Property(e => e.TopSix)
                .HasDefaultValueSql("'N'")
                .HasComment("為了首六功能所創建新top");
            entity.Property(e => e.Type).HasComment("接案類型");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.Website).HasComment("網站");
            entity.Property(e => e.XoopsUid).HasComment("指定[設計師群組]的會員");
        });

        modelBuilder.Entity<HdesignerAddtion>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.Property(e => e.Sn).HasComment("pk");
            entity.Property(e => e.Aname).HasComment("設計師姓名");
            entity.Property(e => e.Atype).HasComment("專業證照");
            entity.Property(e => e.Avalue1).HasComment("證照名稱");
            entity.Property(e => e.Avalue2).HasComment("證照號");
            entity.Property(e => e.HdesignerId).HasComment("設計師id");
        });

        modelBuilder.Entity<HdesignerTmp>(entity =>
        {
            entity.HasKey(e => e.HdesignerId).HasName("PRIMARY");

            entity.Property(e => e.HdesignerId).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Area).HasComment("接案坪數");
            entity.Property(e => e.Awards).HasComment("獲獎紀錄");
            entity.Property(e => e.AwardsLogo).HasComment("獎項logo");
            entity.Property(e => e.AwardsName).HasComment("獎項名稱");
            entity.Property(e => e.Background).HasComment("設計師背景圖");
            entity.Property(e => e.BackgroundMobile).HasComment("手機板背景圖");
            entity.Property(e => e.Blog).HasComment("其他網址連結");
            entity.Property(e => e.Budget).HasComment("接案預算");
            entity.Property(e => e.Career).HasComment("相關經歷");
            entity.Property(e => e.Charge).HasComment("收費方式");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CoordinateX).HasComment("座標X");
            entity.Property(e => e.CoordinateY).HasComment("座標Y");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.DesignerMail).HasComment("通知設計師的email");
            entity.Property(e => e.Dorder)
                .HasDefaultValueSql("'99999'")
                .HasComment("排序");
            entity.Property(e => e.Fax).HasComment("傳真");
            entity.Property(e => e.Fbpageurl).HasComment("FB page URL");
            entity.Property(e => e.Guarantee).HasComment("幸福經紀人");
            entity.Property(e => e.Idea).HasComment("設計理念");
            entity.Property(e => e.ImgPath).HasComment("頭像");
            entity.Property(e => e.IsSend).HasComment("寄送數目");
            entity.Property(e => e.License).HasComment("相關證照");
            entity.Property(e => e.LineLink).HasComment("LINE URL");
            entity.Property(e => e.Location).HasComment("設計師所在區域");
            entity.Property(e => e.Mail).HasComment("E-mail");
            entity.Property(e => e.MaxBudget).HasComment("預算最大值");
            entity.Property(e => e.MinBudget).HasComment("預算最小值");
            entity.Property(e => e.MobileOrder)
                .HasDefaultValueSql("'99999'")
                .HasComment("手機板排序");
            entity.Property(e => e.Name).HasComment("設計師名稱");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Payment).HasComment("付費方式");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Position).HasComment("品牌定位");
            entity.Property(e => e.Region).HasComment("接案區域");
            entity.Property(e => e.SalesMail).HasComment("業務的email");
            entity.Property(e => e.ServicePhone).HasComment("免付費電話");
            entity.Property(e => e.Special).HasComment("特殊接案");
            entity.Property(e => e.Style).HasComment("接案風格");
            entity.Property(e => e.Taxid).HasComment("公司統編");
            entity.Property(e => e.Title).HasComment("公司抬頭");
            entity.Property(e => e.Top)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("置頂(N: 否 / Y:是");
            entity.Property(e => e.Type).HasComment("接案類型");
            entity.Property(e => e.Website).HasComment("網站");
            entity.Property(e => e.XoopsUid).HasComment("指定[設計師群組]的會員");
        });

        modelBuilder.Entity<Hevent>(entity =>
        {
            entity.HasKey(e => e.HeventId).HasName("PRIMARY");

            entity.Property(e => e.HeventId).HasComment("pk");
            entity.Property(e => e.Answer).HasComment("答案");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.Desc).HasComment("活動內文描述");
            entity.Property(e => e.Eend).HasComment("活動結束時間");
            entity.Property(e => e.Elink).HasComment("活動連結");
            entity.Property(e => e.Elogo).HasComment("logo路徑");
            entity.Property(e => e.Estart).HasComment("活動開始時間");
            entity.Property(e => e.HprizeId).HasComment("獎品ID");
            entity.Property(e => e.Question).HasComment("問題");
            entity.Property(e => e.Title).HasComment("活動標題");
            entity.Property(e => e.Type).HasComment("活動類別");
            entity.Property(e => e.WinnerHtml).HasComment("得獎名單內文");
            entity.Property(e => e.WinnerTitle).HasComment("得獎名單標題");
        });

        modelBuilder.Entity<HeventAttend>(entity =>
        {
            entity.Property(e => e.Answer).HasComment("答案");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.HeventId).HasComment("pk");
            entity.Property(e => e.Uid).HasComment("使用者ID");
        });

        modelBuilder.Entity<Hext1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Ext1Date).HasComment("外網上稿日期");
            entity.Property(e => e.Ext1Title).HasComment("文章標題");
            entity.Property(e => e.Ext1TypeId).HasComment("分類");
            entity.Property(e => e.Ext1Url).HasComment("網址");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.Mcount).HasComment("點擊數");
        });

        modelBuilder.Entity<Hguestbook>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");
        });

        modelBuilder.Entity<HhhHp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.City).HasComment("城市");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("創建時間");
            entity.Property(e => e.Email).HasComment("郵件");
            entity.Property(e => e.HpBuilderId).HasComment("建案編號");
            entity.Property(e => e.IsAgree).HasComment("(1:同意收到 0:沒勾選)");
            entity.Property(e => e.IsRequest)
                .HasDefaultValueSql("'1'")
                .HasComment("(1:現在有需求 0:之後考量)");
            entity.Property(e => e.Name).HasComment("名字");
            entity.Property(e => e.Phone).HasComment("手機");
            entity.Property(e => e.Region).HasComment("地區");
        });

        modelBuilder.Entity<Hhp>(entity =>
        {
            entity.HasKey(e => e.Hpid).HasName("PRIMARY");
        });

        modelBuilder.Entity<HmillionAttend>(entity =>
        {
            entity.HasKey(e => new { e.Uid, e.Year })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.Applytime).HasDefaultValueSql("'0000-00-00 00:00:00'");
        });

        modelBuilder.Entity<HmillionPrize>(entity =>
        {
            entity.HasKey(e => e.PrizeId).HasName("PRIMARY");
        });

        modelBuilder.Entity<HmillionVendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PRIMARY");
        });

        modelBuilder.Entity<HmillionVote>(entity =>
        {
            entity.HasKey(e => new { e.VoteDate, e.VoterUid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<Hmyfav>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Desc).HasComment("描述");
            entity.Property(e => e.PageTitle).HasComment("頁面名稱");
            entity.Property(e => e.Uid).HasComment("使用者ID");
            entity.Property(e => e.Url).HasComment("連結");
        });

        modelBuilder.Entity<HmyfavMobile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Hnewspaper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Email).HasComment("EMAIL");
            entity.Property(e => e.Failcount).HasComment("發送狀態( 成功:0 失敗:1)");
            entity.Property(e => e.IsSend).HasComment("發送次數");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("是否開啟(否:0 是:1)");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
        });

        modelBuilder.Entity<HomepageSet>(entity =>
        {
            entity.HasKey(e => e.PsId).HasName("PRIMARY");

            entity.Property(e => e.PsId).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.EndTime).HasComment("結束時間");
            entity.Property(e => e.InnerSort).HasComment("元素排序");
            entity.Property(e => e.MappingId).HasComment("主題編號(廣告&影片&內容)");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("上線狀態(N:關Y:開)	");
            entity.Property(e => e.OuterSet).HasComment("FK : outer_site_set.oss_id");
            entity.Property(e => e.StartTime).HasComment("開始時間");
            entity.Property(e => e.ThemeType).HasComment("主題類型(case:個案 video:影音 column:居家 product:產品 ad:內容廣告 fans:粉絲推薦 week:本週推薦)");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<Hoplog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Action).HasComment("執行動作");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.Opdesc).HasComment("操作描述");
            entity.Property(e => e.PageName).HasComment("頁面名稱");
            entity.Property(e => e.Sqlcmd).HasComment("執行SQL");
            entity.Property(e => e.Uid).HasComment("帳號ID");
            entity.Property(e => e.Uname).HasComment("帳號");
        });

        modelBuilder.Entity<Hprize>(entity =>
        {
            entity.HasKey(e => e.HprizeId).HasName("PRIMARY");

            entity.Property(e => e.HprizeId).HasComment("pk");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("獎品說明");
            entity.Property(e => e.Logo).HasComment("獎品logo");
            entity.Property(e => e.Title).HasComment("獎品名稱");
        });

        modelBuilder.Entity<HprizeEvent>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Hproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Brief).HasComment("簡介");
            entity.Property(e => e.Cate1).HasComment("分類");
            entity.Property(e => e.Cate2).HasComment("分類2");
            entity.Property(e => e.Cate3).HasComment("分類3");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.Cover).HasComment("封面圖");
            entity.Property(e => e.Descr).HasComment("敘述");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.IsSend).HasComment("寄送次數");
            entity.Property(e => e.Model).HasComment("型號");
            entity.Property(e => e.Name).HasComment("產品名稱");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Space).HasComment("使用空間");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("更新時間");
            entity.Property(e => e.Wherebuy).HasComment("哪裡買");
        });

        modelBuilder.Entity<HproductImg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.BuilderProductId).HasComment("建案id");
            entity.Property(e => e.Descr).HasComment("敘述");
            entity.Property(e => e.HproductId).HasComment("產品ID");
            entity.Property(e => e.IsCover).HasComment("是否設定為封面(0:否1:是)");
            entity.Property(e => e.Name).HasComment("產品圖片名稱");
            entity.Property(e => e.OrderNo).HasComment("排序");
            entity.Property(e => e.Title).HasComment("產品圖片標題");
        });

        modelBuilder.Entity<Hprog>(entity =>
        {
            entity.HasKey(e => e.HprogId).HasName("PRIMARY");

            entity.Property(e => e.HprogId).HasComment("pk");
            entity.Property(e => e.ChanId).HasComment("頻道id");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Debut).HasComment("首播時間");
            entity.Property(e => e.Onoff).HasComment("是否露出這個節目(0:否1:是) ");
            entity.Property(e => e.ProgType).HasComment("節目種類");
        });

        modelBuilder.Entity<HprogChan>(entity =>
        {
            entity.HasKey(e => e.ChanId).HasName("PRIMARY");

            entity.Property(e => e.ChanId).HasComment("pk");
            entity.Property(e => e.Broadcast).HasComment("撥出頻道");
            entity.Property(e => e.Clogo).HasComment("頻道logo");
            entity.Property(e => e.Cname).HasComment("頻道名稱");
            entity.Property(e => e.CnameS)
                .HasDefaultValueSql("'gstv'")
                .HasComment("頻道名稱縮寫");
            entity.Property(e => e.Corder).HasComment("排序");
            entity.Property(e => e.Onoff).HasComment("開/關");
            entity.Property(e => e.Premiere).HasComment("首播時間");
            entity.Property(e => e.Replay).HasComment("重播時間");
        });

        modelBuilder.Entity<HprogTbl>(entity =>
        {
            entity.Property(e => e.HprogId).HasComment("pk");
            entity.Property(e => e.Order).HasComment("排序");
        });

        modelBuilder.Entity<HprogUnit>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.Property(e => e.Sn).HasComment("pk");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("影片說明");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.HcolumnId).HasComment("專欄ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.HvideoId).HasComment("影片ID");
            entity.Property(e => e.Info).HasComment("影片資訊");
            entity.Property(e => e.Timg).HasComment("影片縮圖");
            entity.Property(e => e.Title).HasComment("單元名稱");
        });

        modelBuilder.Entity<Hpublish>(entity =>
        {
            entity.HasKey(e => e.HpublishId).HasName("PRIMARY");

            entity.Property(e => e.HpublishId).HasComment("pk");
            entity.Property(e => e.Author).HasComment("作者");
            entity.Property(e => e.Desc).HasComment("描述");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Pdate).HasComment("出版日期");
            entity.Property(e => e.Recommend).HasComment("推薦數");
            entity.Property(e => e.Title).HasComment("名稱");
            entity.Property(e => e.Type).HasComment("書籍類別");
            entity.Property(e => e.Viewed).HasComment("觀看數");
        });

        modelBuilder.Entity<Htopic>(entity =>
        {
            entity.HasKey(e => e.HtopicId).HasName("PRIMARY");

            entity.Property(e => e.HtopicId).HasComment("pk");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("主題敘述");
            entity.Property(e => e.IsSend).HasComment("寄送次數");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:否1:是)");
            entity.Property(e => e.StrarrHcaseId).HasComment("個案IDs");
            entity.Property(e => e.StrarrHcolumnId).HasComment("專欄IDs");
            entity.Property(e => e.Title).HasComment("主題名稱");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.Viewed).HasComment("觀看數");
        });

        modelBuilder.Entity<Htopic2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Desc).HasComment("主題敘述");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:否1:是)");
            entity.Property(e => e.StrarrHcaseId).HasComment("個案ID");
            entity.Property(e => e.StrarrHcolumnId).HasComment("專欄ID");
            entity.Property(e => e.StrarrHdesignerId).HasComment("設計師ID");
            entity.Property(e => e.StrarrHvideoId).HasComment("影音ID");
            entity.Property(e => e.Title).HasComment("名稱");
        });

        modelBuilder.Entity<Hvideo>(entity =>
        {
            entity.HasKey(e => e.HvideoId).HasName("PRIMARY");

            entity.Property(e => e.HvideoId).HasComment("pk");
            entity.Property(e => e.BuilderProductId).HasComment("建案id");
            entity.Property(e => e.BuilderProductOtherId).HasComment("建案其他影片id");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("影音說明");
            entity.Property(e => e.DisplayDatetime).HasDefaultValueSql("'1994-01-26 14:30:00'");
            entity.Property(e => e.FeeTag).HasComment("計算機使用");
            entity.Property(e => e.ForChina).HasComment("大陸地區觀看");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.HcolumnId).HasComment("專欄ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.Iframe).HasComment("外部影片");
            entity.Property(e => e.IsSend).HasComment("發送次數");
            entity.Property(e => e.Name).HasComment("名稱");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'1'")
                .HasComment("上線狀態(0:關1:開)");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sponsor).HasComment("贊助");
            entity.Property(e => e.StyleTag).HasComment("計算機使用");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.TagVpattern).HasComment("標籤-空間格局");
            entity.Property(e => e.TagVtype).HasComment("標籤-單元類型");
            entity.Property(e => e.ThumbnailTime).HasComment("強制設定縮圖");
            entity.Property(e => e.Title).HasComment("影音標題");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.VfileType).HasComment("影音類型");
            entity.Property(e => e.Viewed).HasComment("觀看數");
            entity.Property(e => e.Wxh).HasComment("寬x高");
        });

        modelBuilder.Entity<HvideoBak>(entity =>
        {
            entity.HasKey(e => e.HvideoId).HasName("PRIMARY");

            entity.Property(e => e.HvideoId).HasComment("pk");
            entity.Property(e => e.BuilderProductId).HasComment("建案id");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("影音說明");
            entity.Property(e => e.FeeTag).HasComment("計算機使用");
            entity.Property(e => e.ForChina).HasComment("大陸地區觀看");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.HcolumnId).HasComment("專欄ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.Iframe).HasComment("外部影片");
            entity.Property(e => e.IsSend).HasComment("發送次數");
            entity.Property(e => e.Name).HasComment("名稱");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sponsor).HasComment("贊助");
            entity.Property(e => e.StyleTag).HasComment("計算機使用");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.TagVpattern).HasComment("標籤-空間格局");
            entity.Property(e => e.TagVtype).HasComment("標籤-單元類型");
            entity.Property(e => e.ThumbnailTime).HasComment("強制設定縮圖");
            entity.Property(e => e.Title).HasComment("影音標題");
            entity.Property(e => e.VfileType).HasComment("影音類型");
            entity.Property(e => e.Viewed).HasComment("觀看數");
            entity.Property(e => e.Wxh).HasComment("寬x高");
        });

        modelBuilder.Entity<HvideoTmp>(entity =>
        {
            entity.HasKey(e => e.HvideoId).HasName("PRIMARY");

            entity.Property(e => e.HvideoId).HasComment("pk");
            entity.Property(e => e.BuilderProductId).HasComment("建案id");
            entity.Property(e => e.Clicks).HasComment("點擊數");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("影音說明");
            entity.Property(e => e.FeeTag).HasComment("計算機使用");
            entity.Property(e => e.ForChina).HasComment("大陸地區觀看");
            entity.Property(e => e.HbrandId).HasComment("廠商ID");
            entity.Property(e => e.HcaseId).HasComment("個案ID");
            entity.Property(e => e.HcolumnId).HasComment("專欄ID");
            entity.Property(e => e.HdesignerId).HasComment("設計師ID");
            entity.Property(e => e.Iframe).HasComment("外部影片");
            entity.Property(e => e.IsSend).HasComment("發送次數");
            entity.Property(e => e.Name).HasComment("名稱");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.Sponsor).HasComment("贊助");
            entity.Property(e => e.StyleTag).HasComment("計算機使用");
            entity.Property(e => e.Tag).HasComment("Tag");
            entity.Property(e => e.TagDatetime).HasComment("Tag更新時間");
            entity.Property(e => e.TagVpattern).HasComment("標籤-空間格局");
            entity.Property(e => e.TagVtype).HasComment("標籤-單元類型");
            entity.Property(e => e.ThumbnailTime).HasComment("強制設定縮圖");
            entity.Property(e => e.Title).HasComment("影音標題");
            entity.Property(e => e.VfileType).HasComment("影音類型");
            entity.Property(e => e.Viewed).HasComment("觀看數");
            entity.Property(e => e.Wxh).HasComment("寬x高");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PRIMARY");

            entity.Property(e => e.ImageMimetype).HasDefaultValueSql("''");
            entity.Property(e => e.ImageName).HasDefaultValueSql("''");
            entity.Property(e => e.ImageNicename).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Imagebody>(entity =>
        {
            entity.Property(e => e.ImageId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Imagecategory>(entity =>
        {
            entity.HasKey(e => e.ImgcatId).HasName("PRIMARY");

            entity.Property(e => e.ImgcatName).HasDefaultValueSql("''");
            entity.Property(e => e.ImgcatStoretype).HasDefaultValueSql("''");
            entity.Property(e => e.ImgcatType)
                .HasDefaultValueSql("''")
                .IsFixedLength();
        });

        modelBuilder.Entity<Imgset>(entity =>
        {
            entity.HasKey(e => e.ImgsetId).HasName("PRIMARY");

            entity.Property(e => e.ImgsetName).HasDefaultValueSql("''");
            entity.Property(e => e.ImgsetRefid).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<ImgsetTplsetLink>(entity =>
        {
            entity.HasIndex(e => e.TplsetName, "tplset_name").HasAnnotation("MySql:IndexPrefixLength", new[] { 10 });

            entity.Property(e => e.TplsetName).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Imgsetimg>(entity =>
        {
            entity.HasKey(e => e.ImgsetimgId).HasName("PRIMARY");

            entity.Property(e => e.ImgsetimgFile).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Guid).HasComment("GUID");
            entity.Property(e => e.InsTime).HasComment("建立時間");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.Num).HasComment("編號");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("處理狀態(N:未處理 / Y:已處理)");
            entity.Property(e => e.Type).HasComment("類型");
            entity.Property(e => e.UpdTime).HasComment("處理時間");
            entity.Property(e => e.Url).HasComment("網址");
        });

        modelBuilder.Entity<MobileInnerSetup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("pk");
            entity.Property(e => e.BeltImg).HasComment("內頁腰帶廣告圖(");
            entity.Property(e => e.BeltUrl).HasComment("內頁腰帶廣告網址");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Mid).HasName("PRIMARY");

            entity.HasIndex(e => e.Name, "name").HasAnnotation("MySql:IndexPrefixLength", new[] { 15 });

            entity.Property(e => e.Dirname).HasDefaultValueSql("''");
            entity.Property(e => e.Name).HasDefaultValueSql("''");
            entity.Property(e => e.Version).HasDefaultValueSql("'100'");
        });

        modelBuilder.Entity<Newblock>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PRIMARY");

            entity.Property(e => e.BlockType)
                .HasDefaultValueSql("''")
                .IsFixedLength();
            entity.Property(e => e.CType)
                .HasDefaultValueSql("''")
                .IsFixedLength();
            entity.Property(e => e.Dirname).HasDefaultValueSql("''");
            entity.Property(e => e.EditFunc).HasDefaultValueSql("''");
            entity.Property(e => e.FuncFile).HasDefaultValueSql("''");
            entity.Property(e => e.Name).HasDefaultValueSql("''");
            entity.Property(e => e.Options).HasDefaultValueSql("''");
            entity.Property(e => e.ShowFunc).HasDefaultValueSql("''");
            entity.Property(e => e.Template).HasDefaultValueSql("''");
            entity.Property(e => e.Title).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Online>(entity =>
        {
            entity.Property(e => e.OnlineIp).HasDefaultValueSql("''");
            entity.Property(e => e.OnlineUid).HasDefaultValueSql("'0'");
            entity.Property(e => e.OnlineUname).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<OuterSiteSet>(entity =>
        {
            entity.HasKey(e => e.OssId).HasName("PRIMARY");

            entity.Property(e => e.OssId).HasComment("pk");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.MaxRow)
                .HasDefaultValueSql("'6'")
                .HasComment("最大行數");
            entity.Property(e => e.MoreCopy).HasComment("區塊右上角更多(文字)");
            entity.Property(e => e.MoreUrl).HasComment("更多連結");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("上線狀態(N:關Y:開)");
            entity.Property(e => e.Sort).HasComment("區塊位置");
            entity.Property(e => e.ThemeType).HasComment("主題類型(case:個案 video:影音 column:居家 product:產品 ad:內容廣告 fans:粉絲推薦 week:本週推薦)");
            entity.Property(e => e.Title).HasComment("區塊標題");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<PhotosEdm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasComment("姓名");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否訂閱(0:否 / 1:是");
            entity.Property(e => e.UpdateTime).HasComment("建立時間");
        });

        modelBuilder.Entity<Precise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Company).HasComment("公司名稱");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("郵件");
            entity.Property(e => e.Identity).HasComment("身份別");
            entity.Property(e => e.Mobile).HasComment("手機號碼");
            entity.Property(e => e.Name).HasComment("中文姓名");
        });

        modelBuilder.Entity<PrivMsg>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("PRIMARY");

            entity.Property(e => e.FromUserid).HasDefaultValueSql("'0'");
            entity.Property(e => e.Subject).HasDefaultValueSql("''");
            entity.Property(e => e.ToUserid).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<ProgList>(entity =>
        {
            entity.HasKey(e => e.ProgListId).HasName("PRIMARY");

            entity.Property(e => e.ProgListId).HasComment("pk");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否露出(N:否Y:是)");
            entity.Property(e => e.ProgDate).HasComment("播出日期");
            entity.Property(e => e.ProgName).HasComment("節目名稱");
            entity.Property(e => e.ProgTime).HasComment("播出時間");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<ProgVideo>(entity =>
        {
            entity.HasKey(e => e.ProgId).HasName("PRIMARY");

            entity.Property(e => e.ProgId).HasComment("pk");
            entity.Property(e => e.ChanId).HasComment("頻道名稱");
            entity.Property(e => e.ChanName).HasComment("頻道名稱");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.DisplayDate).HasComment("播出時間");
            entity.Property(e => e.DisplayDatetime).HasComment("播出時間(精確到小時)");
            entity.Property(e => e.Gid).HasComment("FK:youtube_group.gid");
            entity.Property(e => e.GroupName).HasComment("群組名稱");
            entity.Property(e => e.IsSend)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(N:未寄出 / Y:已寄出)");
            entity.Property(e => e.MailContent).HasComment("郵件內容");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否露出(N:否Y:是) ");
            entity.Property(e => e.SendDatetime).HasComment("寄出時間");
            entity.Property(e => e.Sort).HasComment("排序");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
            entity.Property(e => e.Yid).HasComment("FK : youtube_list.yid");
            entity.Property(e => e.YoutubeVideoId).HasComment("youtube影片id");
        });

        modelBuilder.Entity<Psychological>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Content).HasComment("內容");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Icon).HasComment("圖示位置");
            entity.Property(e => e.IconForFb).HasComment("FB分享圖示");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否開啟(1:開 / 0:關");
            entity.Property(e => e.OptionA).HasComment("選項A_內容");
            entity.Property(e => e.OptionALink).HasComment("選項A連結");
            entity.Property(e => e.OptionATitle).HasComment("選項A_標題");
            entity.Property(e => e.OptionB).HasComment("選項B_內容");
            entity.Property(e => e.OptionBLink).HasComment("選項B連結");
            entity.Property(e => e.OptionBTitle).HasComment("選項B_標題");
            entity.Property(e => e.OptionC).HasComment("選項C_內容");
            entity.Property(e => e.OptionCLink).HasComment("選項C連結");
            entity.Property(e => e.OptionCTitle).HasComment("選項C_標題");
            entity.Property(e => e.OptionD).HasComment("選項D_內容");
            entity.Property(e => e.OptionDLink).HasComment("選項D連結");
            entity.Property(e => e.OptionDTitle).HasComment("選項D_標題");
            entity.Property(e => e.Title).HasComment("標題");
            entity.Property(e => e.Viewed)
                .HasDefaultValueSql("'1'")
                .HasComment("人氣數");
        });

        modelBuilder.Entity<QuotationBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Content).HasComment("品牌方案內容");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Creator).HasComment("建立者");
            entity.Property(e => e.IsDel)
                .HasDefaultValueSql("'0'")
                .HasComment("是否刪除:1(刪除) 0:保留");
            entity.Property(e => e.LastUpdate).HasComment("最後更新者");
            entity.Property(e => e.Name).HasComment("客戶名稱");
            entity.Property(e => e.Note).HasComment("備註");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasComment("金額(未稅/萬)");
            entity.Property(e => e.QuotaTime)
                .HasDefaultValueSql("curdate()")
                .HasComment("報價時間");
            entity.Property(e => e.Sales).HasComment("負責業務");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<QuotationDesigner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Content).HasComment("品牌方案內容");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Creator).HasComment("建立者");
            entity.Property(e => e.IsDel)
                .HasDefaultValueSql("'0'")
                .HasComment("是否刪除:1(刪除) 0:保留");
            entity.Property(e => e.LastUpdate).HasComment("最後更新者");
            entity.Property(e => e.Name).HasComment("客戶名稱");
            entity.Property(e => e.Note).HasComment("備註");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasComment("金額(未稅/萬)");
            entity.Property(e => e.QuotaTime)
                .HasDefaultValueSql("curdate()")
                .HasComment("報價時間");
            entity.Property(e => e.Sales).HasComment("負責業務");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.RankId).HasName("PRIMARY");

            entity.Property(e => e.RankMax).HasDefaultValueSql("'0'");
            entity.Property(e => e.RankMin).HasDefaultValueSql("'0'");
            entity.Property(e => e.RankTitle).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<RenovationReuqest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Area).HasComment("所在縣市");
            entity.Property(e => e.Bldsno).HasComment("全國設計師id");
            entity.Property(e => e.Budget).HasComment("裝修預算");
            entity.Property(e => e.Ctime).HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("電子郵件");
            entity.Property(e => e.IsFb)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否為FB帳號(Y:是 / N:否");
            entity.Property(e => e.Loan).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Mode).HasComment("房屋型態");
            entity.Property(e => e.Name).HasComment("姓名");
            entity.Property(e => e.Pattern).HasComment("裝修格局");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Pin).HasComment("裝修坪數");
            entity.Property(e => e.SendDatetime).HasComment("寄出時間");
            entity.Property(e => e.SendStatus)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("是否寄出(Y:是 / N:否");
            entity.Property(e => e.Sex)
                .IsFixedLength()
                .HasComment("性別(M:男性 / F:女性)");
            entity.Property(e => e.Style).HasComment("風格需求");
            entity.Property(e => e.Time).HasComment("希望裝修時間");
            entity.Property(e => e.Type).HasComment("房屋類型");
            entity.Property(e => e.UtmSource).HasComment("UTM_SOURCE");
        });

        modelBuilder.Entity<RssLinetoday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間 (2019-06-21 11:00:00)");
            entity.Property(e => e.Date).HasComment("推送日期 (2019-06-21)");
            entity.Property(e => e.Hvideo).HasComment("MSN_影音編號");
            entity.Property(e => e.UpdateTime).HasComment("修改時間 (2019-06-21 11:00:00)");
        });

        modelBuilder.Entity<RssMsn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間 (2019-06-21 11:00:00)");
            entity.Property(e => e.Date).HasComment("推送日期 (2019-06-21)");
            entity.Property(e => e.Hcase).HasComment("MSN_個案編號");
            entity.Property(e => e.Hcolumn).HasComment("MSN_專欄編號");
            entity.Property(e => e.UpdateTime).HasComment("修改時間 (2019-06-21 11:00:00)");
        });

        modelBuilder.Entity<RssTransfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.Datetime).HasComment("建立時間");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.Num).HasComment("編號");
            entity.Property(e => e.Source)
                .IsFixedLength()
                .HasComment("來自");
            entity.Property(e => e.Type)
                .IsFixedLength()
                .HasComment("類型(column : 專欄 / case : 個案)");
        });

        modelBuilder.Entity<RssYahoo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間 (2019-06-21 11:00:00)");
            entity.Property(e => e.Date).HasComment("推送日期 (2019-06-21)");
            entity.Property(e => e.Hcase).HasComment("個案編號 (111,222,333)");
            entity.Property(e => e.Hcolumn).HasComment("專欄編號 (111,222,333)");
            entity.Property(e => e.UpdateTime).HasComment("修改時間 (2019-06-21 11:00:00)");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.SearchId).HasName("PRIMARY");

            entity.ToTable("search_history", tb => tb.HasComment("關鍵字搜尋紀錄"));

            entity.Property(e => e.SearchId).HasComment("id");
            entity.Property(e => e.DateAdded).HasComment("日期");
            entity.Property(e => e.Keyword).HasComment("關鍵字");
            entity.Property(e => e.TodayCount).HasComment("次數");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessId).HasName("PRIMARY");

            entity.Property(e => e.SessId)
                .HasDefaultValueSql("''")
                .HasComment("seeionID");
            entity.Property(e => e.SessData).HasComment("session資料");
            entity.Property(e => e.SessIp)
                .HasDefaultValueSql("''")
                .HasComment("IP");
            entity.Property(e => e.SessUpdated).HasComment("更新時間");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Address).HasComment("地址");
            entity.Property(e => e.Border).HasComment("排序");
            entity.Property(e => e.Bspace).HasComment("已從頁面拿掉");
            entity.Property(e => e.Bstype).HasComment("已從頁面拿掉");
            entity.Property(e => e.Btype).HasComment("產品類別");
            entity.Property(e => e.CreatTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Desc).HasComment("敘述");
            entity.Property(e => e.Email).HasComment("廠商EMAIL");
            entity.Property(e => e.Fbpageurl).HasComment("臉書");
            entity.Property(e => e.Gchoice).HasComment("幸福空間推薦");
            entity.Property(e => e.History).HasComment("品牌故事");
            entity.Property(e => e.HvideoId).HasComment("影片ID");
            entity.Property(e => e.Imported).HasComment("進口品牌(改版後用)");
            entity.Property(e => e.Intro).HasComment("敘述");
            entity.Property(e => e.Logo).HasComment("logo");
            entity.Property(e => e.Logo2).HasComment("logo2");
            entity.Property(e => e.Onoff).HasComment("上線狀態(0:關閉 1:開啟)");
            entity.Property(e => e.Phone).HasComment("電話");
            entity.Property(e => e.Recommend).HasComment("推薦");
            entity.Property(e => e.SalesEmail).HasComment("負責業務EMAIL");
            entity.Property(e => e.Title).HasComment("廠商名稱");
            entity.Property(e => e.Vr360Id).HasComment("vr306_ID");
            entity.Property(e => e.Website).HasComment("網站");
        });

        modelBuilder.Entity<ShopImg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Descr).HasComment("描述");
            entity.Property(e => e.IsCover).HasComment("是否設定為封面(0:否1:是)");
            entity.Property(e => e.Name).HasComment("圖片名稱");
            entity.Property(e => e.OrderNo).HasComment("排序");
            entity.Property(e => e.ShopId).HasComment("廠商ID");
            entity.Property(e => e.Title).HasComment("標題");
        });

        modelBuilder.Entity<ShortUrl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.Code).HasComment("縮指碼");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Memo).HasComment("備註");
            entity.Property(e => e.Photo).HasComment("自定義標題");
            entity.Property(e => e.Preview)
                .HasDefaultValueSql("'[]'")
                .HasComment("預覽文案");
            entity.Property(e => e.ReplaceTitle).HasComment("自定義圖片");
            entity.Property(e => e.TrackA).HasComment("FB追蹤參數");
            entity.Property(e => e.TrackB).HasComment("第二組追蹤參數");
            entity.Property(e => e.TrackC).HasComment("第三組追蹤參數");
            entity.Property(e => e.TrackD).HasComment("第四組追蹤參數");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
            entity.Property(e => e.Url).HasComment("網址");
        });

        modelBuilder.Entity<ShortUrlLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Ip).HasComment("ip");
            entity.Property(e => e.IsMobile)
                .HasDefaultValueSql("'1'")
                .HasComment("是否是否為手機(0:否 1:是)");
            entity.Property(e => e.ShortUrlId).HasComment("short_url pk");
            entity.Property(e => e.Track).HasComment("追蹤碼");
            entity.Property(e => e.UserId).HasComment("會員ID");
        });

        modelBuilder.Entity<SiteSetup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("pk");
            entity.Property(e => e.AllSearchTag).HasComment("全站搜尋關鍵字");
            entity.Property(e => e.BrandSearchTag).HasComment("廠商搜尋關鍵字");
            entity.Property(e => e.CaseSearchTag).HasComment("案例搜尋關鍵字");
            entity.Property(e => e.ColumnSearchTag).HasComment("專欄搜尋關鍵字");
            entity.Property(e => e.Decoquery).HasComment("查證照(查詢總數)");
            entity.Property(e => e.DesignerSearchTag).HasComment("設計師搜尋關鍵字");
            entity.Property(e => e.DesktopPreVideo).HasComment("前導影片上傳");
            entity.Property(e => e.ForumFilter).HasComment("討論區過濾字");
            entity.Property(e => e.MobileHotCase).HasComment("手機版人氣好宅IDs");
            entity.Property(e => e.MobileHotVideo).HasComment("手機版夯影音IDs");
            entity.Property(e => e.MobileIndexTab1).HasComment("TAB1名稱");
            entity.Property(e => e.MobileIndexTab1Designer).HasComment("TAB1推薦設計師IDs");
            entity.Property(e => e.MobileIndexTab2).HasComment("TAB2名稱");
            entity.Property(e => e.MobileIndexTab2Designer).HasComment("TAB2推薦設計師IDs");
            entity.Property(e => e.MobileIndexTab3).HasComment("TAB3名稱");
            entity.Property(e => e.MobileIndexTab3Designer).HasComment("TAB3推薦設計師IDs");
            entity.Property(e => e.MobileIndexTab4).HasComment("TAB4名稱");
            entity.Property(e => e.MobileIndexTab4Designer).HasComment("TAB4推薦設計師IDs");
            entity.Property(e => e.MobileIndexTab5).HasComment("TAB5名稱");
            entity.Property(e => e.MobileIndexTab5Designer).HasComment("TAB5推薦設計師IDs");
            entity.Property(e => e.PreviewVideo).HasComment("預覽影音");
            entity.Property(e => e.VideoSearchTag).HasComment("影音搜尋關鍵字");
            entity.Property(e => e.Vimeo).HasComment("vimeo或youtube網址");
            entity.Property(e => e.VimeoCover).HasComment("蓋掉vimeo的縮圖");
            entity.Property(e => e.YoutubeId).HasComment("首頁youtube影片ID");
            entity.Property(e => e.YoutubeTitle).HasComment("首頁影片標題");
        });

        modelBuilder.Entity<Smile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Code).HasDefaultValueSql("''");
            entity.Property(e => e.Emotion).HasDefaultValueSql("''");
            entity.Property(e => e.SmileUrl).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<SmsHistory>(entity =>
        {
            entity.HasKey(e => e.Seq).HasName("PRIMARY");

            entity.ToTable("sms_history", tb => tb.HasComment("簡訊傳送紀錄"));

            entity.Property(e => e.Response).HasComment("回應碼");
            entity.Property(e => e.Status).HasComment("0:待查詢 1:完成 2:失敗 ");
        });

        modelBuilder.Entity<Tplfile>(entity =>
        {
            entity.HasKey(e => e.TplId).HasName("PRIMARY");

            entity.HasIndex(e => new { e.TplTplset, e.TplFile1 }, "tpl_tplset").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 10 });

            entity.Property(e => e.TplDesc).HasDefaultValueSql("''");
            entity.Property(e => e.TplFile1).HasDefaultValueSql("''");
            entity.Property(e => e.TplModule).HasDefaultValueSql("''");
            entity.Property(e => e.TplTplset).HasDefaultValueSql("''");
            entity.Property(e => e.TplType).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Tplset>(entity =>
        {
            entity.HasKey(e => e.TplsetId).HasName("PRIMARY");

            entity.Property(e => e.TplsetDesc).HasDefaultValueSql("''");
            entity.Property(e => e.TplsetName).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Tplsource>(entity =>
        {
            entity.HasKey(e => e.TplId).HasName("PRIMARY");

            entity.Property(e => e.TplId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PRIMARY");

            entity.Property(e => e.Uid).HasComment("pk");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否啟用(0:未啟用 / 1:已啟用");
            entity.Property(e => e.Actkey).HasDefaultValueSql("''");
            entity.Property(e => e.Birthday).HasComment("生日");
            entity.Property(e => e.Email)
                .HasDefaultValueSql("''")
                .HasComment("EMAIL");
            entity.Property(e => e.EmailStatus).HasComment("會員信箱狀態(X:信箱無效 / B:不寄發)");
            entity.Property(e => e.FacebookToken).HasComment("fb_token");
            entity.Property(e => e.ForumBlock)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("討論區黑名單(N: 否 / Y:是)");
            entity.Property(e => e.GoogleToken).HasComment("google_token");
            entity.Property(e => e.Guid).HasComment("全球唯一碼");
            entity.Property(e => e.LastLogin).HasComment("最後登入時間");
            entity.Property(e => e.LastLoginDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("最後登入時間 (yyyy-mm-dd)");
            entity.Property(e => e.Level).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("''")
                .HasComment("姓名");
            entity.Property(e => e.NotifyMethod).HasDefaultValueSql("'1'");
            entity.Property(e => e.Pass)
                .HasDefaultValueSql("''")
                .HasComment("密碼");
            entity.Property(e => e.Posts).HasDefaultValueSql("'0'");
            entity.Property(e => e.Sex)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasComment("性別(N:不限 / M:男 / F:女)");
            entity.Property(e => e.Theme).HasDefaultValueSql("''");
            entity.Property(e => e.TimezoneOffset).HasDefaultValueSql("'8.0'");
            entity.Property(e => e.Umode).HasDefaultValueSql("''");
            entity.Property(e => e.Uname)
                .HasDefaultValueSql("''")
                .HasComment("帳號");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("更新時間");
            entity.Property(e => e.Url).HasDefaultValueSql("''");
            entity.Property(e => e.UserAim).HasDefaultValueSql("''");
            entity.Property(e => e.UserAvatar)
                .HasDefaultValueSql("'blank.gif'")
                .HasComment("頭像");
            entity.Property(e => e.UserFrom)
                .HasDefaultValueSql("''")
                .HasComment("地址");
            entity.Property(e => e.UserIcq).HasDefaultValueSql("''");
            entity.Property(e => e.UserIntrest)
                .HasDefaultValueSql("''")
                .HasComment("聯絡電話");
            entity.Property(e => e.UserMailok).HasDefaultValueSql("'1'");
            entity.Property(e => e.UserMsnm).HasDefaultValueSql("''");
            entity.Property(e => e.UserOcc).HasDefaultValueSql("''");
            entity.Property(e => e.UserRegdate).HasComment("註冊時間");
            entity.Property(e => e.UserRegdateDatetime)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("註冊時間(yyyy-mm-dd)");
            entity.Property(e => e.UserYim).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<UserFavorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("pk");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("收藏狀態(0:收藏1:移除收藏)");
            entity.Property(e => e.TableId).HasComment("我的最愛ID");
            entity.Property(e => e.Type).HasComment("我的最愛類型");
            entity.Property(e => e.UserId).HasComment("使用者ID");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.Property(e => e.LogId).HasComment("pk");
            entity.Property(e => e.BrandId)
                .HasDefaultValueSql("'0'")
                .HasComment("廠商ID");
            entity.Property(e => e.DesignerId)
                .HasDefaultValueSql("'0'")
                .HasComment("設計師ID");
            entity.Property(e => e.InsTime).HasComment("瀏覽時間");
            entity.Property(e => e.Ip).HasComment("IP");
            entity.Property(e => e.PageType).HasComment("瀏覽網頁");
            entity.Property(e => e.TableId).HasComment("網頁對應ID");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("'0'")
                .HasComment("使用者ID");
        });

        modelBuilder.Entity<UsersAuth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.AuthDatetime).HasComment("認證時間");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Guid).HasComment("認證碼");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'0'")
                .IsFixedLength()
                .HasComment("是否啟用(0:未啟用 / 1:已啟用");
            entity.Property(e => e.Uid).HasComment("FK : _users.uid");
        });

        modelBuilder.Entity<Xoopscomment>(entity =>
        {
            entity.HasKey(e => e.ComId).HasName("PRIMARY");

            entity.HasIndex(e => e.ComTitle, "com_title").HasAnnotation("MySql:IndexPrefixLength", new[] { 40 });

            entity.Property(e => e.ComExparams).HasDefaultValueSql("''");
            entity.Property(e => e.ComIcon).HasDefaultValueSql("''");
            entity.Property(e => e.ComIp).HasDefaultValueSql("''");
            entity.Property(e => e.ComItemid).HasDefaultValueSql("'0'");
            entity.Property(e => e.ComPid).HasDefaultValueSql("'0'");
            entity.Property(e => e.ComRootid).HasDefaultValueSql("'0'");
            entity.Property(e => e.ComTitle).HasDefaultValueSql("''");
            entity.Property(e => e.ComUid).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Xoopsnotification>(entity =>
        {
            entity.HasKey(e => e.NotId).HasName("PRIMARY");

            entity.Property(e => e.NotCategory).HasDefaultValueSql("''");
            entity.Property(e => e.NotEvent).HasDefaultValueSql("''");
            entity.Property(e => e.NotItemid).HasDefaultValueSql("'0'");
            entity.Property(e => e.NotUid).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<YoutubeGroup>(entity =>
        {
            entity.HasKey(e => e.Gid).HasName("PRIMARY");

            entity.Property(e => e.Gid).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Name).HasComment("群組名稱(代號)");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'Y'")
                .IsFixedLength()
                .HasComment("是否關閉(N:否 / Y:是");
        });

        modelBuilder.Entity<YoutubeGroupDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("PK");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Gid).HasComment("FK : youtube_group.gid");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'Y'")
                .IsFixedLength()
                .HasComment("是否開啟(Y:是 / N:否");
            entity.Property(e => e.Sort).HasComment("排序");
            entity.Property(e => e.Yid).HasComment("FK :  youtube.yid");
        });

        modelBuilder.Entity<YoutubeList>(entity =>
        {
            entity.HasKey(e => e.Yid).HasName("PRIMARY");

            entity.Property(e => e.Yid).HasComment("pk");
            entity.Property(e => e.BuilderId).HasComment("建商ID");
            entity.Property(e => e.ChannelId).HasComment("頻道編號(Youtube");
            entity.Property(e => e.CreateTime).HasComment("建立時間");
            entity.Property(e => e.Description).HasComment("影片敘述");
            entity.Property(e => e.HbrandId).HasComment("廠商id");
            entity.Property(e => e.HdesignerId).HasComment("設計師pk");
            entity.Property(e => e.IsDel).HasComment("是否刪除(0 : 否 / 1 : 是)");
            entity.Property(e => e.Onoff)
                .HasDefaultValueSql("'Y'")
                .IsFixedLength()
                .HasComment("是否開啟(Y:開 / N:關");
            entity.Property(e => e.PageToken).HasComment("換頁token");
            entity.Property(e => e.PublishedTime).HasComment("影片發布時間");
            entity.Property(e => e.Title).HasComment("影片標題");
            entity.Property(e => e.UpdateTime).HasComment("更新時間");
            entity.Property(e => e.YoutubeImg).HasComment("圖片位置");
            entity.Property(e => e.YoutubeVideoId).HasComment("youtube影片id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
