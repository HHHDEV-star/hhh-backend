using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.Extensions.Logging;

namespace hhh.infrastructure.Logging;

/// <summary>
/// 寫一筆 _hoplog 資料。採 best-effort：失敗會 log 但不拋例外，
/// 避免 audit 故障阻斷主流程。
/// </summary>
internal sealed class OperationLogWriter : IOperationLogWriter
{
    private readonly XoopsContext _db;
    private readonly IOperationContextAccessor _context;
    private readonly ILogger<OperationLogWriter> _logger;

    // _hoplog 欄位長度（對應 scaffold 出來的 StringLength attribute）
    private const int PageNameMaxLength = 32;
    private const int UnameMaxLength = 32;
    private const int IpMaxLength = 128;

    public OperationLogWriter(
        XoopsContext db,
        IOperationContextAccessor context,
        ILogger<OperationLogWriter> logger)
    {
        _db = db;
        _context = context;
        _logger = logger;
    }

    public async Task WriteAsync(
        string pageName,
        OperationAction action,
        string opdesc,
        string sqlcmd = "",
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entry = new Hoplog
            {
                Uid = _context.UserId ?? 0,
                Uname = Truncate(_context.UserName, UnameMaxLength) ?? string.Empty,
                PageName = Truncate(pageName, PageNameMaxLength),
                Action = MapAction(action),
                Opdesc = opdesc ?? string.Empty,
                Sqlcmd = sqlcmd ?? string.Empty,
                Ip = Truncate(_context.ClientIp, IpMaxLength),
                // CreatTime 欄位在 DB 端通常是 CURRENT_TIMESTAMP；
                // 這邊還是顯式給值，避免某些 server mode 下預設值未套用。
                CreatTime = DateTime.Now,
            };

            _db.Hoplogs.Add(entry);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // audit 寫入失敗不應該影響業務流程
            _logger.LogError(
                ex,
                "Failed to write operation log. PageName={PageName}, Action={Action}, Opdesc={Opdesc}",
                pageName,
                action,
                opdesc);
        }
    }

    private static string MapAction(OperationAction action) => action switch
    {
        OperationAction.Create  => "新增",
        OperationAction.Update  => "修改",
        OperationAction.Delete  => "刪除",
        OperationAction.Replace => "置換",
        _ => throw new ArgumentOutOfRangeException(nameof(action), action, null),
    };

    private static string? Truncate(string? value, int max)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= max ? value : value.Substring(0, max);
    }
}
