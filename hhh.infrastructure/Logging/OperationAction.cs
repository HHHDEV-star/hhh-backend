namespace hhh.infrastructure.Logging;

/// <summary>
/// 對應舊 PHP _save_log() 的四種動作字串（_hoplog.action varchar(4)）
/// </summary>
public enum OperationAction
{
    /// <summary>新增</summary>
    Create,

    /// <summary>修改</summary>
    Update,

    /// <summary>刪除</summary>
    Delete,

    /// <summary>置換（REPLACE INTO）</summary>
    Replace,
}
