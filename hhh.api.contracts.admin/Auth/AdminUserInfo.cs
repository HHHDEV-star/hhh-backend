using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hhh.api.contracts.admin.Auth
{

    /// <summary>
    /// 登入者基本資料（對應 admin 資料表，但排除敏感欄位）
    /// </summary>
    public class AdminUserInfo
    {
        public uint Id { get; set; }
        public string? Account { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Tel { get; set; }
        public string? AllowPage { get; set; }
    }
}
