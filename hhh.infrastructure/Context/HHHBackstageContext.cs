using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hhh.infrastructure.Dto.HHHBackstage;

namespace hhh.infrastructure.Context;

public partial class HHHBackstageContext : DbContext
{
    public HHHBackstageContext(DbContextOptions<HHHBackstageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AclMenuGroup> AclMenuGroups { get; set; }

    public virtual DbSet<AclMenuPath> AclMenuPaths { get; set; }

    public virtual DbSet<AclProject> AclProjects { get; set; }

    public virtual DbSet<AclUser> AclUsers { get; set; }

    public virtual DbSet<AclUserAccess> AclUserAccesses { get; set; }

    public virtual DbSet<Backend> Backends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<AclMenuGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("主鍵");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Icon).HasComment("顯示圖標");
            entity.Property(e => e.IsShow)
                .HasDefaultValueSql("'1'")
                .HasComment("是否顯示在目錄(0:否,1:是)");
            entity.Property(e => e.Name).HasComment("目錄群組名稱");
            entity.Property(e => e.SortNum).HasComment("群組排序");
            entity.Property(e => e.UpdateDate).HasComment("編輯時間");
        });

        modelBuilder.Entity<AclMenuPath>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("acl_menu_path", tb => tb.HasComment("連結路徑資料"));

            entity.Property(e => e.Id).HasComment("主鍵");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.IsShow)
                .HasDefaultValueSql("'1'")
                .HasComment("是否顯示在目錄(0:否,1:是)");
            entity.Property(e => e.Name).HasComment("功能名稱");
            entity.Property(e => e.Path).HasComment("功能路徑");
            entity.Property(e => e.ProjectId).HasComment("功能所屬專案編號");
            entity.Property(e => e.SortNum).HasComment("功能排序");
            entity.Property(e => e.UpdateDate).HasComment("編輯時間");
        });

        modelBuilder.Entity<AclProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("acl_projects", tb => tb.HasComment("專案資料"));

            entity.Property(e => e.Id).HasComment("主鍵");
            entity.Property(e => e.App).HasComment("專案app資料夾名稱");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Name).HasComment("專案名稱");
            entity.Property(e => e.UpdateDate).HasComment("編輯時間");
        });

        modelBuilder.Entity<AclUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("acl_users", tb => tb.HasComment("使用者帳號資訊"));

            entity.Property(e => e.Id).HasComment("主鍵");
            entity.Property(e => e.Account).HasComment("帳號");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.Email).HasComment("電子郵件");
            entity.Property(e => e.IsDel)
                .HasDefaultValueSql("'0'")
                .HasComment("是否刪除(0:否 1:是)");
            entity.Property(e => e.IsExecuteSales)
                .HasDefaultValueSql("'0'")
                .HasComment("業務身份(0:否 / 1:是)");
            entity.Property(e => e.IsRemote)
                .HasDefaultValueSql("'0'")
                .HasComment("是否可以遠端(0:否 / 1:可");
            entity.Property(e => e.LoginDate)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasComment("登入時間");
            entity.Property(e => e.Name).HasComment("使用者名稱");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("'''無'''")
                .HasComment("職位");
            entity.Property(e => e.Pwd).HasComment("密碼");
            entity.Property(e => e.UpdateDate).HasComment("編輯時間");
        });

        modelBuilder.Entity<AclUserAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("acl_user_access", tb => tb.HasComment("使用者訪問權限"));

            entity.Property(e => e.Id).HasComment("主鍵");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("建立時間");
            entity.Property(e => e.MenuPathId).HasComment("目錄路徑編號");
            entity.Property(e => e.UserId).HasComment("使用者編號");
        });

        modelBuilder.Entity<Backend>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IpAddress })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
