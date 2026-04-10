using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[PrimaryKey("ModuleId", "BlockId")]
[Table("_block_module_link")]
public partial class BlockModuleLink
{
    [Key]
    [Column("block_id", TypeName = "mediumint unsigned")]
    public uint BlockId { get; set; }

    [Key]
    [Column("module_id")]
    public short ModuleId { get; set; }
}
