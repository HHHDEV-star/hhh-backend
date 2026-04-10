using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_xoopscomments")]
[Index("ComItemid", Name = "com_itemid")]
[Index("ComPid", Name = "com_pid")]
[Index("ComStatus", Name = "com_status")]
[Index("ComUid", Name = "com_uid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Xoopscomment
{
    [Key]
    [Column("com_id", TypeName = "mediumint unsigned")]
    public uint ComId { get; set; }

    [Column("com_pid", TypeName = "mediumint unsigned")]
    public uint ComPid { get; set; }

    [Column("com_rootid", TypeName = "mediumint unsigned")]
    public uint ComRootid { get; set; }

    [Column("com_modid")]
    public ushort ComModid { get; set; }

    [Column("com_itemid", TypeName = "mediumint unsigned")]
    public uint ComItemid { get; set; }

    [Column("com_icon")]
    [StringLength(25)]
    public string ComIcon { get; set; } = null!;

    [Column("com_created")]
    public uint ComCreated { get; set; }

    [Column("com_modified")]
    public uint ComModified { get; set; }

    [Column("com_uid", TypeName = "mediumint unsigned")]
    public uint ComUid { get; set; }

    [Column("com_ip")]
    [StringLength(15)]
    public string ComIp { get; set; } = null!;

    [Column("com_title")]
    public string ComTitle { get; set; } = null!;

    [Column("com_text", TypeName = "text")]
    public string? ComText { get; set; }

    [Column("com_sig")]
    public byte ComSig { get; set; }

    [Column("com_status")]
    public byte ComStatus { get; set; }

    [Column("com_exparams")]
    [StringLength(255)]
    public string ComExparams { get; set; } = null!;

    [Column("dohtml")]
    public byte Dohtml { get; set; }

    [Column("dosmiley")]
    public byte Dosmiley { get; set; }

    [Column("doxcode")]
    public byte Doxcode { get; set; }

    [Column("doimage")]
    public byte Doimage { get; set; }

    [Column("dobr")]
    public byte Dobr { get; set; }
}
