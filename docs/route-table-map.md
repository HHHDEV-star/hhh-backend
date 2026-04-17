# 後台 API 路由 × 資料表 對照表

> 本文件由每個 admin API 路由反向追蹤至它在 `XoopsContext` / `HHHBackstageContext` / `HhhApiContext` 中實際讀寫的資料表，供「改哪張表會動到哪幾支 API」或「這支 API 會動到哪幾張表」的雙向查閱。
>
> **資料表名以 entity 類別上的 `[Table("...")]` attribute 為準**：Xoops 舊 CMS 表以底線（`_`）開頭（例：`_hcase`、`_hoplog`、`_users`），後期新增的業務表無前綴（例：`admin`、`builder_product`）。跨 DB 的表以 `DbName.table` 格式標示（例：`HHHBackstage.acl_users`）。
>
> **後端總覽**：19 個 controller，216 個 endpoint，52 張 Xoops 表 + 5 張跨 DB 表（合計 57 張）。
> **前端覆蓋**：50 個頁面呼叫 137 條 route，涵蓋 50 張資料表；7 張表尚無前端頁面（見末尾「需留意的異常」）。

---

## AdvertiseController (`/api/advertise`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/advertise/ads/list | `AdService.GetListAsync` | `_had` |
| POST | /api/advertise/ads | `AdService.CreateAsync` | `_had`, `_hoplog` |
| PUT | /api/advertise/ads/{adid} | `AdService.UpdateAsync` | `_had`, `_hoplog` |
| PUT | /api/advertise/ads/{adid}/logo | `AdService.UpdateLogoAsync` | `_had` |
| DELETE | /api/advertise/ads/{adid} | `AdService.DeleteAsync` | `_had`, `_hoplog` |

## AgentsController (`/api/agents`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/agents/list | `AgentService.GetListAsync` | `agent_form` |
| DELETE | /api/agents/{agentId} | `AgentService.DeleteAsync` | `agent_form` |
| GET | /api/agents/{agentId}/files | `AgentService.GetFilesAsync` | `agent_files` |
| DELETE | /api/agents/{agentId}/files/{fileId} | `AgentService.DeleteFileAsync` | `agent_files` |

## AuthController (`/api/auth`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| POST | /api/auth/login | `AuthService.LoginAsync` | `admin` |

## AwardsController (`/api/awards`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/awards/hawards/list | `HawardService.GetListAsync` | `_hawards`, `_hcase`, `_hdesigner` |
| GET | /api/awards/hawards/{id} | `HawardService.GetByIdAsync` | `_hawards`, `_hcase`, `_hdesigner` |
| POST | /api/awards/hawards | `HawardService.CreateAsync` | `_hawards`, `_hcase`, `_hdesigner`, `_hoplog` |
| PUT | /api/awards/hawards/{id} | `HawardService.UpdateAsync` | `_hawards`, `_hcase`, `_hdesigner`, `_hoplog` |
| DELETE | /api/awards/hawards/{id} | `HawardService.DeleteAsync` | `_hawards`, `_hoplog` |
| GET | /api/awards/hprizes/list | `HprizeService.GetListAsync` | `_hprize` |
| GET | /api/awards/hprizes/{id} | `HprizeService.GetByIdAsync` | `_hprize` |
| POST | /api/awards/hprizes | `HprizeService.CreateAsync` | `_hoplog`, `_hprize` |
| PUT | /api/awards/hprizes/{id} | `HprizeService.UpdateAsync` | `_hoplog`, `_hprize` |
| DELETE | /api/awards/hprizes/{id} | `HprizeService.DeleteAsync` | `_hoplog`, `_hprize` |
| GET | /api/awards/hcontests/list | `HcontestService.GetListAsync` | `_hcontest` |
| GET | /api/awards/hcontests/{id} | `HcontestService.GetByIdAsync` | `_hcontest` |
| POST | /api/awards/hcontests | `HcontestService.CreateAsync` | `_hcontest`, `_hoplog` |
| PUT | /api/awards/hcontests/{id} | `HcontestService.UpdateAsync` | `_hcontest`, `_hoplog` |
| DELETE | /api/awards/hcontests/{id} | `HcontestService.DeleteAsync` | `_hcontest`, `_hoplog` |

## BrokersController (`/api/brokers`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/brokers/agents/list | `AgentService.GetListAsync` | `agent_form` |
| GET | /api/brokers/agents/{agentId} | `AgentService.GetByIdAsync` | `agent_form` |
| GET | /api/brokers/agents/{agentId}/files | `AgentService.GetFilesAsync` | `agent_files` |
| POST | /api/brokers/agents | `AgentService.CreateAsync` | `agent_form` |
| PUT | /api/brokers/agents/{agentId} | `AgentService.UpdateAsync` | `agent_form` |
| DELETE | /api/brokers/agent-files/{fileId} | `AgentService.DeleteFileAsync` | `agent_files` |
| DELETE | /api/brokers/agents/{agentId} | `AgentService.DeleteAsync` | `agent_form` |
| GET | /api/brokers/calculator-requests/list | `CalculatorRequestService.GetListAsync` | `calculator_request` |
| GET | /api/brokers/calculators/list | `CalculatorService.GetListAsync` | `calculator` |
| DELETE | /api/brokers/deco-request-files | `DecoRequestService.DeleteFilesAsync` | `_hoplog`, `deco_request_files` |
| DELETE | /api/brokers/deco-requests | `DecoRequestService.BatchSoftDeleteAsync` | `_hoplog`, `deco_request` |
| GET | /api/brokers/deco-requests/list | `DecoRequestService.GetListAsync` | `deco_request` |
| GET | /api/brokers/deco-requests/{seq} | `DecoRequestService.GetByIdAsync` | `deco_request` |
| GET | /api/brokers/deco-requests/{seq}/files | `DecoRequestService.GetFilesAsync` | `deco_request_files` |
| POST | /api/brokers/deco-requests | `DecoRequestService.CreateAsync` | `_hoplog`, `deco_request` |
| POST | /api/brokers/deco-requests/{seq}/files | `DecoRequestService.CreateFilesAsync` | `_hoplog`, `deco_request`, `deco_request_files` |
| POST | /api/brokers/deco-requests/{seq}/set-price | `DecoRequestService.SetPriceAsync` | `_hoplog`, `deco_request` |
| PUT | /api/brokers/deco-requests/{seq} | `DecoRequestService.UpdateAsync` | `_hoplog`, `deco_request` |
| GET | /api/brokers/renovations/list | `RenovationService.GetListAsync` | `deco_record`, `renovation_reuqest` |

## CallInController (`/api/call-ins`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/call-ins/list | `CallinDataService.GetListAsync` | `callin_data` |
| POST | /api/call-ins | `CallinDataService.BatchCreateAsync` | `callin_data` |

## ContentController (`/api/content`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/content/hpublishes/list | `HpublishService.GetListAsync` | `_hpublish` |
| GET | /api/content/hpublishes/{id} | `HpublishService.GetByIdAsync` | `_hpublish` |
| POST | /api/content/hpublishes | `HpublishService.CreateAsync` | `_hpublish` |
| PUT | /api/content/hpublishes/{id} | `HpublishService.UpdateAsync` | `_hpublish` |
| DELETE | /api/content/hpublishes/{id} | `HpublishService.DeleteAsync` | `_hpublish` |
| GET | /api/content/htopic2s/list | `Htopic2Service.GetListAsync` | `_htopic2` |
| GET | /api/content/htopic2s/{id} | `Htopic2Service.GetByIdAsync` | `_htopic2` |
| POST | /api/content/htopic2s | `Htopic2Service.CreateAsync` | `_htopic2` |
| PUT | /api/content/htopic2s/{id} | `Htopic2Service.UpdateAsync` | `_htopic2` |
| DELETE | /api/content/htopic2s/{id} | `Htopic2Service.DeleteAsync` | `_htopic2` |

## DesignersController (`/api/designers`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/designers/hdesigners/list | `HdesignerService.GetListAsync` | `_hdesigner` |
| GET | /api/designers/hdesigners/select-list | `HdesignerService.GetSelectListAsync` | `_hdesigner` |
| GET | /api/designers/hdesigners/{id} | `HdesignerService.GetByIdAsync` | `_hdesigner` |
| POST | /api/designers/hdesigners | `HdesignerService.CreateAsync` | `_hdesigner` |
| PUT | /api/designers/hdesigners/{id} | `HdesignerService.UpdateAsync` | `_hdesigner` |
| PUT | /api/designers/hdesigners/sort-order | `HdesignerService.UpdateSortOrderAsync` | `_hdesigner` |
| PUT | /api/designers/hdesigners/mobile-sort-order | `HdesignerService.UpdateMobileSortOrderAsync` | `_hdesigner` |
| GET | /api/designers/hcases/list | `HcaseService.GetListAsync` | `_hcase`, `_hcase_img`, `_hdesigner` |
| GET | /api/designers/hcases/{id} | `HcaseService.GetByIdAsync` | `_hcase`, `_hdesigner` |
| POST | /api/designers/hcases | `HcaseService.CreateAsync` | `_hcase`, `_hdesigner` |
| PUT | /api/designers/hcases/{id} | `HcaseService.UpdateAsync` | `_hcase`, `_hdesigner` |
| PUT | /api/designers/hcases/sort-order | `HcaseService.UpdateSortOrderAsync` | `_hcase`, `_hdesigner` |

## EditorialController (`/api/editorial`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/editorial/cases/list | `EditorialCaseService.GetListAsync` | `_hcase`, `_hdesigner` |
| GET | /api/editorial/cases/{id} | `EditorialCaseService.GetByIdAsync` | `_hcase`, `_hdesigner` |
| POST | /api/editorial/cases | `EditorialCaseService.CreateAsync` | `_hcase` |
| PUT | /api/editorial/cases/{id} | `EditorialCaseService.UpdateAsync` | `_hcase` |
| GET | /api/editorial/columns/list | `EditorialColumnService.GetListAsync` | `_hcolumn` |
| GET | /api/editorial/columns/{id} | `EditorialColumnService.GetByIdAsync` | `_hcolumn` |
| POST | /api/editorial/columns | `EditorialColumnService.CreateAsync` | `_hcolumn` |
| PUT | /api/editorial/columns/{id} | `EditorialColumnService.UpdateAsync` | `_hcolumn` |
| GET | /api/editorial/topics/list | `HtopicService.GetListAsync` | `_htopic` |
| GET | /api/editorial/topics/{id} | `HtopicService.GetByIdAsync` | `_htopic` |
| POST | /api/editorial/topics | `HtopicService.CreateAsync` | `_htopic` |
| PUT | /api/editorial/topics/{id} | `HtopicService.UpdateAsync` | `_htopic` |
| DELETE | /api/editorial/topics/{id} | `HtopicService.DeleteAsync` | `_htopic` |

## MainController (`/api/main`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/main/search/keywords | `SearchService.GetKeywordTagsAsync` | `site_setup` |
| GET | /api/main/search/hot-keywords | `SearchService.GetHotKeywordsAsync` | `search_history` |
| GET | /api/main/search/autocomplete | `SearchService.GetAutocompleteAsync` | `_hbrand`, `_hdesigner` |
| GET | /api/main/search/backend | `SearchService.SearchBackendAsync` | `_hbrand`, `_hcase`, `_hcolumn`, `_hdesigner`, `_hproduct`, `_hvideo` |
| GET | /api/main/execute-forms/list | `ExecuteFormService.GetListAsync` | `execute_form` |
| GET | /api/main/execute-forms/{exfId} | `ExecuteFormService.GetByIdAsync` | `execute_form` |
| POST | /api/main/execute-forms | `ExecuteFormService.CreateAsync` | `execute_form` |
| PUT | /api/main/execute-forms/{exfId} | `ExecuteFormService.UpdateAsync` | `execute_form` |
| DELETE | /api/main/execute-forms/{exfId} | `ExecuteFormService.SoftDeleteAsync` | `execute_form` |

## MarketingController (`/api/marketing`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/marketing/case-seo/list | `CaseSeoService.GetListAsync` | `_hcase` |
| GET | /api/marketing/column-seo/list | `ColumnSeoService.GetListAsync` | `_hcolumn` |
| GET | /api/marketing/product-seo/list | `ProductSeoService.GetListAsync` | `_hproduct` |
| PUT | /api/marketing/case-seo | `CaseSeoService.BatchUpdateAsync` | `_hcase` |
| PUT | /api/marketing/case-seo/images | `CaseSeoService.BatchUpdateImageAsync` | `_hcase` |
| PUT | /api/marketing/column-seo | `ColumnSeoService.BatchUpdateAsync` | `_hcolumn` |
| PUT | /api/marketing/column-seo/images | `ColumnSeoService.BatchUpdateImageAsync` | `_hcolumn` |
| PUT | /api/marketing/product-seo | `ProductSeoService.BatchUpdateAsync` | `_hproduct` |
| PUT | /api/marketing/product-seo/images | `ProductSeoService.BatchUpdateImageAsync` | `_hproduct` |

## MembersController (`/api/members`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/members/users/list | `UserService.GetListAsync` | `_users` |
| GET | /api/members/users/{id:int} | `UserService.GetByIdAsync` | `_users` |
| POST | /api/members/users | `UserService.CreateAsync` | `_users` |
| PUT | /api/members/users/{id:int} | `UserService.UpdateAsync` | `_users` |

## PlanningController (`/api/planning`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/planning/channels/list | `ProgramVideoService.GetChannelListAsync` | `_hprog_chan` |
| GET | /api/planning/channels/{chanId:int} | `ProgramVideoService.GetChannelAsync` | `_hprog_chan` |
| GET | /api/planning/hvideos/list | `HvideoService.GetListAsync` | `_hbrand`, `_hcase`, `_hcolumn`, `_hdesigner`, `_hvideo` |
| GET | /api/planning/program-list/list | `ProgramVideoService.GetProgramListAsync` | `prog_list` |
| GET | /api/planning/program-videos/list | `ProgramVideoService.GetListAsync` | `prog_video`, `youtube_list` |
| GET | /api/planning/youtube/list | `YoutubeService.GetListAsync` | `youtube_list` |
| GET | /api/planning/youtube-group-details/list | `YoutubeManagementService.GetGroupDetailListAsync` | `youtube_group`, `youtube_group_detail`, `youtube_list` |
| GET | /api/planning/youtube-groups/dropdown | `YoutubeManagementService.GetGroupDropdownAsync` | `youtube_group` |
| GET | /api/planning/youtube-groups/list | `YoutubeManagementService.GetGroupListAsync` | `youtube_group` |
| POST | /api/planning/channels | `ProgramVideoService.CreateChannelAsync` | `_hprog_chan` |
| POST | /api/planning/program-videos/generate-from-group | `ProgramVideoService.GenerateFromGroupAsync` | `prog_video`, `youtube_group`, `youtube_group_detail` |
| POST | /api/planning/program-list/approve | `ProgramVideoService.ApproveProgramListAsync` | `prog_list` |
| POST | /api/planning/youtube | `YoutubeService.CreateAsync` | `youtube_list` |
| POST | /api/planning/youtube/import-by-id | `YoutubeManagementService.ImportByVideoIdAsync` | `youtube_list` |
| POST | /api/planning/youtube/sync | `YoutubeManagementService.SyncChannelAsync` | `youtube_list` |
| POST | /api/planning/youtube-groups | `YoutubeManagementService.CreateGroupAsync` | `youtube_group` |
| PATCH | /api/planning/program-videos/{progId:int}/mail-content | `ProgramVideoService.UpdateMailContentAsync` | `prog_video` |
| PUT | /api/planning/channels/{chanId:int} | `ProgramVideoService.UpdateChannelAsync` | `_hprog_chan` |
| PUT | /api/planning/program-videos/{progId:int} | `ProgramVideoService.UpdateAsync` | `prog_video` |
| PUT | /api/planning/youtube-group-details/{id:int} | `YoutubeManagementService.UpdateGroupDetailAsync` | `youtube_group_detail` |
| PUT | /api/planning/youtube-groups/{gid:int} | `YoutubeManagementService.UpdateGroupAsync` | `youtube_group` |
| PUT | /api/planning/youtube/{yid:int} | `YoutubeService.UpdateAsync` | `youtube_list` |
| DELETE | /api/planning/program-videos/{progId:int} | `ProgramVideoService.DeleteAsync` | `prog_video` |
| DELETE | /api/planning/youtube-group-details/{id:int} | `YoutubeManagementService.DeleteGroupDetailAsync` | `youtube_group_detail` |
| DELETE | /api/planning/youtube/{yid:int} | `YoutubeService.DeleteAsync` | `youtube_list` |

## PlatformController (`/api/platform`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/platform/acl-menu-groups/list | `AclMenuGroupService.GetListAsync` | `HHHBackstage.acl_menu_group` |
| GET | /api/platform/acl-menu-paths/list | `AclMenuPathService.GetListAsync` | `HHHBackstage.acl_menu_path` |
| GET | /api/platform/acl-menu-paths/select-menu-groups | `AclMenuPathService.GetMenuGroupOptionsAsync` | `HHHBackstage.acl_menu_group` |
| GET | /api/platform/acl-menu-paths/select-projects | `AclMenuPathService.GetProjectOptionsAsync` | `HHHBackstage.acl_projects` |
| GET | /api/platform/acl-users/list | `AclUserService.GetListAsync` | `HHHBackstage.acl_users` |
| GET | /api/platform/admins/list | `AdminService.GetListAsync` | `admin` |
| GET | /api/platform/admins/me | `AdminService.GetByIdAsync` | `admin` |
| GET | /api/platform/admins/{id:int} | `AdminService.GetByIdAsync` | `admin` |
| GET | /api/platform/operation-logs/list | `OperationLogService.GetListAsync` | `_hoplog` |
| GET | /api/platform/operation-logs/{id:int} | `OperationLogService.GetByIdAsync` | `_hoplog` |
| GET | /api/platform/system-logs/list | `SystemLogService.GetListAsync` | `HhhApi.rest_backend_logs` |
| POST | /api/platform/acl-menu-groups | `AclMenuGroupService.CreateAsync` | `HHHBackstage.acl_menu_group` |
| POST | /api/platform/acl-menu-paths | `AclMenuPathService.CreateAsync` | `HHHBackstage.acl_menu_path` |
| POST | /api/platform/acl-users | `AclUserService.CreateAsync` | `HHHBackstage.acl_users` |
| POST | /api/platform/admins | `AdminService.CreateAsync` | `admin` |
| PUT | /api/platform/acl-menu-groups/{id:int} | `AclMenuGroupService.UpdateAsync` | `HHHBackstage.acl_menu_group` |
| PUT | /api/platform/acl-menu-paths/{id:int} | `AclMenuPathService.UpdateAsync` | `HHHBackstage.acl_menu_path` |
| PUT | /api/platform/acl-users/{id:int} | `AclUserService.UpdateAsync` | `HHHBackstage.acl_users` |
| PUT | /api/platform/admins/me | `AdminService.UpdateProfileAsync` | `admin` |
| PUT | /api/platform/admins/{id:int} | `AdminService.UpdateAsync` | `admin` |

## ReportsController (`/api/reports`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/reports/video-reports/brands | `VideoReportService.GetBrandReportAsync` | `_hbrand`, `_hvideo` |
| GET | /api/reports/video-reports/designers | `VideoReportService.GetDesignerReportAsync` | `_hdesigner`, `_hvideo` |

## RssController (`/api/rss`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/rss/yahoo/list | `RssYahooService.GetListAsync` | `rss_yahoo` |
| POST | /api/rss/yahoo | `RssYahooService.CreateAsync` | `rss_yahoo` |
| PUT | /api/rss/yahoo/{id:int} | `RssYahooService.UpdateAsync` | `rss_yahoo` |
| GET | /api/rss/msn/list | `RssMsnService.GetListAsync` | `rss_msn` |
| POST | /api/rss/msn | `RssMsnService.CreateAsync` | `rss_msn` |
| PUT | /api/rss/msn/{id:int} | `RssMsnService.UpdateAsync` | `rss_msn` |
| GET | /api/rss/line-today/list | `RssLineTodayService.GetListAsync` | `rss_linetoday` |
| POST | /api/rss/line-today | `RssLineTodayService.CreateAsync` | `rss_linetoday` |
| PUT | /api/rss/line-today/{id:int} | `RssLineTodayService.UpdateAsync` | `rss_linetoday` |
| GET | /api/rss/transfer-logs/list | `RssTransferService.GetLogsAsync` | `rss_transfer` |
| GET | /api/rss/transfer-statistics | `RssTransferService.GetStatisticsAsync` | `rss_transfer` |

## SocialController (`/api/social`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/social/briefs/list | `BriefService.GetListAsync` | `brief` |
| GET | /api/social/decorations/list | `DecorationService.GetListAsync` | `decoration` |
| POST | /api/social/decorations | `DecorationService.CreateAsync` | `decoration` |
| GET | /api/social/decoration2s/list | `Decoration2Service.GetListAsync` | ⚠️ 無 DB 查詢，呼叫外部 API |
| GET | /api/social/forum-articles/list | `ForumService.GetArticleBackListAsync` | `_users`, `forum_article` |
| GET | /api/social/forum-articles/{articleId:int}/replies/list | `ForumService.GetReplyBackListAsync` | `_users`, `forum_article_reply` |
| PUT | /api/social/forum-articles/{articleId:int} | `ForumService.UpdateArticleAsync` | `forum_article` |
| PUT | /api/social/forum-articles/{articleId:int}/seo-image | `ForumService.UpdateSeoImageAsync` | `forum_article` |
| PUT | /api/social/forum-replies/{replyId:int} | `ForumService.UpdateReplyAsync` | `forum_article_reply` |
| GET | /api/social/forum-blocks/list | `ForumService.GetBlockListAsync` | `_users` |
| PUT | /api/social/forum-blocks/{uid:int} | `ForumService.UpdateBlockAsync` | `_users` |
| GET | /api/social/precises/list | `PreciseService.GetListAsync` | `precise` |
| POST | /api/social/precises | `PreciseService.CreateAsync` | `precise` |
| GET | /api/social/products/list | `ProductService.GetListAsync` | `_hproduct` |
| GET | /api/social/products/seo/list | `ProductService.GetSeoListAsync` | `_hproduct` |
| PUT | /api/social/products/{id:int} | `ProductService.UpdateAsync` | `_hproduct` |
| PUT | /api/social/products/{id:int}/seo-image | `ProductService.UpdateSeoImageAsync` | `_hproduct` |
| GET | /api/social/hhh-hps/list | `HhhHpService.GetListAsync` | `hhh_hp` |

## TagsController (`/api/tags`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/tags/hcases/list | `TagService.GetHcaseTagsAsync` | `_hcase`, `_hdesigner` |
| PUT | /api/tags/hcases/{id:int} | `TagService.UpdateHcaseTagAsync` | `_hcase` |
| GET | /api/tags/hcolumns/list | `TagService.GetHcolumnTagsAsync` | `_hcolumn` |
| PUT | /api/tags/hcolumns/{id:int} | `TagService.UpdateHcolumnTagAsync` | `_hcolumn` |
| GET | /api/tags/hvideos/list | `TagService.GetHvideoTagsAsync` | `_hvideo` |
| PUT | /api/tags/hvideos/{id:int} | `TagService.UpdateHvideoTagAsync` | `_hvideo` |
| GET | /api/tags/images/list | `TagService.GetImageTagsAsync` | `_hcase`, `_hcase_img` |
| PUT | /api/tags/images/{id:int} | `TagService.UpdateImageTagAsync` | `_hcase_img` |

## WebSiteController (`/api/website`)

| HTTP | Route | 服務方法 | 使用資料表 |
|---|---|---|---|
| GET | /api/website/builders/list | `BuilderService.GetListAsync` | `builder`, `builder_product` |
| GET | /api/website/builders/dropdown | ⚠️ Controller 直接查 DB | `builder` |
| GET | /api/website/builders/{id:int} | ⚠️ Controller 直接查 DB | `builder` |
| POST | /api/website/builders | ⚠️ Controller 直接操作 DB | `builder` |
| PUT | /api/website/builders/{id:int} | ⚠️ Controller 直接操作 DB | `builder` |
| DELETE | /api/website/builders/{id:int} | ⚠️ Controller 直接操作 DB | `builder` |
| GET | /api/website/builder-products/list | `BuilderProductService.GetListAsync` | `_hcolumn`, `_hvideo`, `builder`, `builder_product` |
| GET | /api/website/builder-products/dropdown | `BuilderProductService.GetDropdownAsync` | `builder_product` |
| GET | /api/website/builder-products/{id:int} | `BuilderProductService.GetByIdAsync` | `_hcolumn`, `_hvideo`, `builder`, `builder_product` |
| POST | /api/website/builder-products | `BuilderProductService.CreateAsync` | `_hcolumn`, `_hvideo`, `builder_product` |
| PUT | /api/website/builder-products/{id:int} | `BuilderProductService.UpdateAsync` | `_hcolumn`, `_hvideo`, `builder_product` |
| DELETE | /api/website/builder-products/{id:int} | `BuilderProductService.DeleteAsync` | `builder_product` |
| GET | /api/website/builder-products/{productId:int}/images | `BuilderProductService.GetImagesAsync` | `_hproduct_img`, `builder_product` |
| POST | /api/website/builder-products/{productId:int}/images | `BuilderProductService.CreateImageAsync` | `_hproduct_img`, `builder_product` |
| PUT | /api/website/builder-products/{productId:int}/images/{id:int} | `BuilderProductService.UpdateImageAsync` | `_hproduct_img`, `builder_product` |
| DELETE | /api/website/builder-products/{productId:int}/images/{id:int} | `BuilderProductService.DeleteImageAsync` | `_hproduct_img`, `builder_product` |
| PUT | /api/website/builder-products/{productId:int}/cover | `BuilderProductService.SetCoverAsync` | `_hproduct_img`, `builder_product` |
| GET | /api/website/deco-records/list | `DecoRecordService.GetListAsync` | `deco_record` |
| PUT | /api/website/deco-records/{bldsno:int} | `DecoRecordService.UpdateAsync` | `deco_record` |
| GET | /api/website/deco-images/list | `DecoImageService.GetListAsync` | `deco_record`, `deco_record_img` |
| PUT | /api/website/deco-images/{id:int}/onoff | `DecoImageService.UpdateOnoffAsync` | `deco_record_img` |
| GET | /api/website/keywords/list | `KeywordService.GetHotKeywordsAsync` | `search_history` |
| GET | /api/website/homepage-inner-sets/list | `HomepageInnerSetService.GetListAsync` | `_had`, `_hcase`, `_hcolumn`, `_hdesigner`, `_hproduct`, `_hvideo`, `homepage_set`, `outer_site_set` |
| POST | /api/website/homepage-inner-sets | `HomepageInnerSetService.CreateAsync` | `homepage_set` |
| PUT | /api/website/homepage-inner-sets/{psId:int} | `HomepageInnerSetService.UpdateAsync` | `homepage_set` |
| DELETE | /api/website/homepage-inner-sets/{psId:int} | `HomepageInnerSetService.DeleteAsync` | `homepage_set` |
| GET | /api/website/site-setup | `SiteSetupService.GetAsync` | `site_setup` |
| PUT | /api/website/site-setup | `SiteSetupService.UpdateAsync` | `site_setup` |
| GET | /api/website/contacts/list | `ContactService.GetListAsync` | `contact` |

---

## 資料表索引

每張表出現一次計 1（跨 route 累計）；同一 route 同一張表不重複計算。

### Xoops 舊 CMS 表（`_` 前綴）

| 資料表 | 被引用次數 |
|---|---|
| `_hdesigner` | 24 |
| `_hcase` | 22 |
| `_hoplog` | 20 |
| `_hcolumn` | 16 |
| `_hvideo` | 11 |
| `_hproduct` | 9 |
| `_users` | 8 |
| `_had` | 6 |
| `_htopic` | 5 |
| `_htopic2` | 5 |
| `_hpublish` | 5 |
| `_hproduct_img` | 5 |
| `_hprize` | 5 |
| `_hcontest` | 5 |
| `_hawards` | 5 |
| `_hprog_chan` | 4 |
| `_hbrand` | 4 |
| `_hcase_img` | 3 |

### Xoops 新業務表（無前綴）

| 資料表 | 被引用次數 |
|---|---|
| `builder_product` | 12 |
| `youtube_list` | 8 |
| `builder` | 8 |
| `deco_request` | 7 |
| `agent_form` | 7 |
| `admin` | 7 |
| `youtube_group` | 6 |
| `prog_video` | 5 |
| `execute_form` | 5 |
| `youtube_group_detail` | 4 |
| `homepage_set` | 4 |
| `deco_record` | 4 |
| `agent_files` | 4 |
| `site_setup` | 3 |
| `rss_yahoo` | 3 |
| `rss_msn` | 3 |
| `rss_linetoday` | 3 |
| `forum_article` | 3 |
| `deco_request_files` | 3 |
| `search_history` | 2 |
| `rss_transfer` | 2 |
| `prog_list` | 2 |
| `precise` | 2 |
| `forum_article_reply` | 2 |
| `decoration` | 2 |
| `deco_record_img` | 2 |
| `callin_data` | 2 |
| `renovation_reuqest` | 1 |
| `outer_site_set` | 1 |
| `hhh_hp` | 1 |
| `contact` | 1 |
| `calculator_request` | 1 |
| `calculator` | 1 |
| `brief` | 1 |

### HHHBackstage DB（ACL 權限系統）

| 資料表 | 被引用次數 |
|---|---|
| `HHHBackstage.acl_menu_group` | 4 |
| `HHHBackstage.acl_users` | 3 |
| `HHHBackstage.acl_menu_path` | 3 |
| `HHHBackstage.acl_projects` | 1 |

### HhhApi DB

| 資料表 | 被引用次數 |
|---|---|
| `HhhApi.rest_backend_logs` | 1 |

---

## 資料表 × 前端頁面 反向索引

> 由「前端頁面 → 匯入 `@/services/hhh/*` 函數 → 對應 API 路由 → 本文件上方該路由使用的資料表」串接而成。取自 `hhh-admin-fe` 專案（Ant Design Pro）。選單名稱取自 `config/routes.ts`（格式：**父選單 › 子選單**）。
>
> 掃描結果：50 個前端頁面呼叫 137 / 215 條 route，涵蓋 50 / 57 張資料表；50/50 頁都對應到 routes.ts 中的選單。
>
> 若某張表的頁面欄顯示「_（無對應頁面）_」，表示沒有任何前端頁面匯入過對應的路由函數 — 可能尚未串接 UI、已被其他 server 端程式使用、或該表僅透過內部寫入。

### Xoops 舊 CMS 表（`_` 前綴）

| 資料表 | 前端頁面（父選單 › 子選單） |
|---|---|
| `_hcolumn` | **客戶開發處 › 建商列表** · `src/pages/customer/builders/index.tsx`<br>**編輯採訪部 › 專欄類別查詢快調** · `src/pages/editorial/column-subtypes/index.tsx`<br>**編輯採訪部 › 專欄文章** · `src/pages/editorial/columns/index.tsx`<br>**行銷企劃部 › 專欄分享設定** · `src/pages/marketing/column-seo/index.tsx`<br>**影音企劃部 › 影音資訊** · `src/pages/video/info/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_hdesigner` | **亞洲設計獎 › 獎項** · `src/pages/asia-design-awards/awards/index.tsx`<br>**編輯採訪部 › 個案資訊** · `src/pages/editorial/cases/index.tsx`<br>**編輯採訪部 › 設計師資訊** · `src/pages/editorial/designers/index.tsx`<br>**行銷企劃部 › 設計師列表** · `src/pages/marketing/designers/index.tsx`<br>**影音企劃部 › 影音資訊** · `src/pages/video/info/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_hcase` | **亞洲設計獎 › 獎項** · `src/pages/asia-design-awards/awards/index.tsx`<br>**編輯採訪部 › 個案資訊** · `src/pages/editorial/cases/index.tsx`<br>**行銷企劃部 › 個案分享設定** · `src/pages/marketing/case-seo/index.tsx`<br>**影音企劃部 › 影音資訊** · `src/pages/video/info/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_hoplog` | **廣告版位露出** · `src/pages/ads/index.tsx`<br>**亞洲設計獎 › 獎項** · `src/pages/asia-design-awards/awards/index.tsx`<br>**亞洲設計獎 › 競圖** · `src/pages/asia-design-awards/contests/index.tsx`<br>**站內收單管理 › 輕裝修需求單** · `src/pages/orders/light-deco/index.tsx` |
| `_hvideo` | **客戶開發處 › 建商列表** · `src/pages/customer/builders/index.tsx`<br>**影音企劃部 › 影音資訊** · `src/pages/video/info/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_users` | **行銷企劃部 › 討論區黑名單** · `src/pages/marketing/forum-block/index.tsx`<br>**行銷企劃部 › 討論區發文管理** · `src/pages/marketing/forum/index.tsx`<br>**後台系統管理 › 會員管理** · `src/pages/system/members/index.tsx` |
| `_had` | **廣告版位露出** · `src/pages/ads/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_hproduct` | **行銷企劃部 › 產品分享設定** · `src/pages/marketing/product-seo/index.tsx`<br>**網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `_hawards` | **亞洲設計獎 › 獎項** · `src/pages/asia-design-awards/awards/index.tsx` |
| `_hbrand` | **影音企劃部 › 影音資訊** · `src/pages/video/info/index.tsx` |
| `_hcontest` | **亞洲設計獎 › 競圖** · `src/pages/asia-design-awards/contests/index.tsx` |
| `_hprog_chan` | **影音企劃部 › TV 節目表資訊** · `src/pages/video/tv-program/index.tsx` |
| `_htopic` | **編輯採訪部 › 主題企劃** · `src/pages/editorial/topics/index.tsx` |
| `_hcase_img` | _（無對應頁面）_ |
| `_hprize` | _（無對應頁面）_ |
| `_hproduct_img` | _（無對應頁面）_ |
| `_hpublish` | _（無對應頁面）_ |
| `_htopic2` | _（無對應頁面）_ |

### Xoops 新業務表

| 資料表 | 前端頁面（父選單 › 子選單） |
|---|---|
| `deco_record` | **經紀人 › 裝修需求單** · `src/pages/agent/renovation/index.tsx`<br>**客戶開發處 › 裝修登記圖片審核** · `src/pages/customer/deco-images/index.tsx`<br>**客戶開發處 › 全國裝修登記查詢** · `src/pages/customer/deco-query/index.tsx` |
| `youtube_group` | **影音企劃部 › 節目表群組資訊** · `src/pages/video/program-group-info/index.tsx`<br>**影音企劃部 › 節目表群組列表** · `src/pages/video/program-group-list/index.tsx`<br>**影音企劃部 › 節目表影片維護** · `src/pages/video/program-video/index.tsx` |
| `youtube_list` | **影音企劃部 › 節目表群組列表** · `src/pages/video/program-group-list/index.tsx`<br>**影音企劃部 › 節目表影片維護** · `src/pages/video/program-video/index.tsx`<br>**影音企劃部 › Youtube 維護** · `src/pages/video/youtube/index.tsx` |
| `agent_form` | **經紀人 › 新增案件** · `src/pages/agent/create/index.tsx`<br>**經紀人 › 案件列表** · `src/pages/agent/list/index.tsx` |
| `rss_transfer` | **編輯採訪部 › RSS 轉換明細** · `src/pages/editorial/rss-lists/index.tsx`<br>**編輯採訪部 › RSS 轉換統計** · `src/pages/editorial/rss-statistics/index.tsx` |
| `youtube_group_detail` | **影音企劃部 › 節目表群組列表** · `src/pages/video/program-group-list/index.tsx`<br>**影音企劃部 › 節目表影片維護** · `src/pages/video/program-video/index.tsx` |
| `agent_files` | **經紀人 › 案件列表** · `src/pages/agent/list/index.tsx` |
| `brief` | **行銷企劃部 › 訂閱 Youtube 活動** · `src/pages/marketing/youtube-subscribe/index.tsx` |
| `builder` | **客戶開發處 › 建商列表** · `src/pages/customer/builders/index.tsx` |
| `builder_product` | **客戶開發處 › 建商列表** · `src/pages/customer/builders/index.tsx` |
| `calculator` | **經紀人 › 舊裝修計算機** · `src/pages/agent/calculator/index.tsx` |
| `calculator_request` | **站內收單管理 › 裝修計算機** · `src/pages/agent/calculator-request/index.tsx` |
| `callin_data` | **客戶開發處 › 0809 通話紀錄** · `src/pages/customer/call-records/index.tsx` |
| `contact` | **網站設計部 › 聯絡我們** · `src/pages/web-design/contact-us/index.tsx` |
| `deco_record_img` | **客戶開發處 › 裝修登記圖片審核** · `src/pages/customer/deco-images/index.tsx` |
| `deco_request` | **站內收單管理 › 輕裝修需求單** · `src/pages/orders/light-deco/index.tsx` |
| `deco_request_files` | **站內收單管理 › 輕裝修需求單** · `src/pages/orders/light-deco/index.tsx` |
| `decoration` | **行銷企劃部 › 全室裝修收名單** · `src/pages/marketing/decoration/index.tsx` |
| `forum_article` | **行銷企劃部 › 討論區發文管理** · `src/pages/marketing/forum/index.tsx` |
| `forum_article_reply` | **行銷企劃部 › 討論區發文管理** · `src/pages/marketing/forum/index.tsx` |
| `hhh_hp` | **行銷企劃部 › 代銷表單** · `src/pages/marketing/agency-form/index.tsx` |
| `homepage_set` | **網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `outer_site_set` | **網站設計部 › 首頁元素** · `src/pages/web-design/index-innerset/index.tsx` |
| `precise` | **行銷企劃部 › 精準名單報名資料** · `src/pages/marketing/precise/index.tsx` |
| `prog_list` | **影音企劃部 › 節目表上線審核** · `src/pages/video/program-review/index.tsx` |
| `prog_video` | **影音企劃部 › 節目表影片維護** · `src/pages/video/program-video/index.tsx` |
| `renovation_reuqest` | **經紀人 › 裝修需求單** · `src/pages/agent/renovation/index.tsx` |
| `rss_linetoday` | **影音企劃部 › RSS LINETODAY 設定** · `src/pages/video/rss-linetoday/index.tsx` |
| `rss_msn` | **編輯採訪部 › RSS MSN 設定** · `src/pages/editorial/rss-msn/index.tsx` |
| `rss_yahoo` | **編輯採訪部 › RSS Yahoo 設定** · `src/pages/editorial/rss-yahoo/index.tsx` |
| `search_history` | **網站設計部 › 站內關鍵字搜尋次數** · `src/pages/web-design/keyword/index.tsx` |
| `site_setup` | **網站設計部 › 網站設定** · `src/pages/web-design/site-setup/index.tsx` |
| `admin` | _（無對應頁面）_ |
| `execute_form` | _（無對應頁面）_ |

### HHHBackstage DB

| 資料表 | 前端頁面（父選單 › 子選單） |
|---|---|
| `HHHBackstage.acl_menu_group` | **後台系統管理 › 目錄管理** · `src/pages/system/menu-paths/index.tsx` |
| `HHHBackstage.acl_menu_path` | **後台系統管理 › 目錄管理** · `src/pages/system/menu-paths/index.tsx` |
| `HHHBackstage.acl_projects` | **後台系統管理 › 目錄管理** · `src/pages/system/menu-paths/index.tsx` |
| `HHHBackstage.acl_users` | **後台系統管理 › 內部帳號管理** · `src/pages/system/admins/index.tsx` |

### HhhApi DB

| 資料表 | 前端頁面（父選單 › 子選單） |
|---|---|
| `HhhApi.rest_backend_logs` | **後台系統管理 › 全站 Log 紀錄** · `src/pages/system/logs/index.tsx` |

---

## 需留意的異常

- **WebSiteController `/api/website/builders/*` 5 支 route 繞過 application 層，直接在 controller 內操作 `XoopsContext`**，違反 CLAUDE.md 硬規則「Controller 不碰 DB」。建議重構為 `BuilderService`。
- **`SocialController` 的 `/api/social/decoration2s/list`** 不查 DB，改呼叫外部 API，表欄位標註為「無 DB 查詢」。
- `_hoplog`（操作紀錄表）被寫入 20 次，是寫入頻率最高的表。凡新增 Create/Update/Delete 類 route 時請維持這個慣例。
- **7 張資料表（`_hcase_img`、`_hprize`、`_hproduct_img`、`_hpublish`、`_htopic2`、`admin`、`execute_form`）尚無任何前端頁面呼叫**。可能是：（a）尚未串接 UI，（b）已棄用、（c）僅被其他服務端邏輯使用。重點排查對象：
  - `admin`：雖然後端有完整 CRUD（`/api/platform/admins/*`），但前端 `system/admins/index.tsx` 實際呼叫的是 `HHHBackstage.acl_users`，兩套帳號體系並存需釐清。
  - `execute_form`：後端 `/api/main/execute-forms/*` 完整 CRUD 已實作，前端尚缺管理頁面。
- **`/api/auth/login`（使用 `admin` 表）未出現在頁面匯入清單中**，因為 `user/login/index.tsx` 走的是 umi `@@/plugin-initialState/initialState` / `model` 流程，而非直接呼叫 `@/services/hhh/auth`。這並不表示未使用，本索引的限制。
