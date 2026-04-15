using System.Text.Json;
using hhh.api.contracts.admin.Social.Decoration2s;
using hhh.api.contracts.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace hhh.application.admin.Social.Decoration2s;

public class Decoration2Service : IDecoration2Service
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ILogger<Decoration2Service> _logger;

    public Decoration2Service(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<Decoration2Service> logger)
    {
        _httpClient = httpClient;
        // 可在 appsettings.json 覆寫外部 API URL
        _apiUrl = configuration["ExternalApi:Decoration2Url"]
                  ?? "https://q.ptt.cx/v_lst";
        _logger = logger;
    }

    public async Task<PagedResponse<Decoration2ListItem>> GetListAsync(
        ListQuery query, CancellationToken ct = default)
    {
        try
        {
            // 舊版 JS 用 POST 呼叫外部 API（無 request body）
            var response = await _httpClient.PostAsync(_apiUrl, null, ct);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(ct);
            var items = JsonSerializer.Deserialize<List<Decoration2ListItem>>(json)
                        ?? [];

            // 外部 API 回傳全部資料，在記憶體內做分頁
            var totalCount = items.Count;
            var page = query.Page;
            var pageSize = query.PageSize;
            var pagedItems = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<Decoration2ListItem>
            {
                Page = page,
                PageSize = pageSize,
                Total = totalCount,
                Items = pagedItems
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "呼叫全室裝修收名單外部 API 失敗：{Url}", _apiUrl);

            // 回傳空分頁，不讓前端爆炸
            return new PagedResponse<Decoration2ListItem>
            {
                Page = query.Page,
                PageSize = query.PageSize
            };
        }
    }
}
