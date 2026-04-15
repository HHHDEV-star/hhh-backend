using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hhh.infrastructure.Dto.HhhApi;

namespace hhh.infrastructure.Context;

public partial class HhhApiContext : DbContext
{
    public HhhApiContext(DbContextOptions<HhhApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RestAccess> RestAccesses { get; set; }

    public virtual DbSet<RestBackendLog> RestBackendLogs { get; set; }

    public virtual DbSet<RestKey> RestKeys { get; set; }

    public virtual DbSet<RestLimit> RestLimits { get; set; }

    public virtual DbSet<RestLog> RestLogs { get; set; }

    public virtual DbSet<RestLogs01> RestLogs01s { get; set; }

    public virtual DbSet<RestLogs02> RestLogs02s { get; set; }

    public virtual DbSet<RestLogs03> RestLogs03s { get; set; }

    public virtual DbSet<RestLogs04> RestLogs04s { get; set; }

    public virtual DbSet<RestLogs05> RestLogs05s { get; set; }

    public virtual DbSet<RestLogs06> RestLogs06s { get; set; }

    public virtual DbSet<RestLogs07> RestLogs07s { get; set; }

    public virtual DbSet<RestLogs08> RestLogs08s { get; set; }

    public virtual DbSet<RestLogs09> RestLogs09s { get; set; }

    public virtual DbSet<RestLogs10> RestLogs10s { get; set; }

    public virtual DbSet<RestLogs11> RestLogs11s { get; set; }

    public virtual DbSet<RestLogs12> RestLogs12s { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<RestAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_access")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Controller)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("controller");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.DateModified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("date_modified");
            entity.Property(e => e.Key)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("key");
        });

        modelBuilder.Entity<RestBackendLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_backend_logs")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Account, "account_index");

            entity.HasIndex(e => e.Method, "method");

            entity.HasIndex(e => e.Uri, "uri");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .HasColumnName("account");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Uri).HasColumnName("uri");
        });

        modelBuilder.Entity<RestKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_keys")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreated).HasColumnName("date_created");
            entity.Property(e => e.IgnoreLimits).HasColumnName("ignore_limits");
            entity.Property(e => e.IpAddresses)
                .HasColumnType("text")
                .HasColumnName("ip_addresses");
            entity.Property(e => e.IsPrivateKey).HasColumnName("is_private_key");
            entity.Property(e => e.Key)
                .HasMaxLength(40)
                .HasColumnName("key");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<RestLimit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_limits")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.HourStarted).HasColumnName("hour_started");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs01>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_01")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.Uri, "uri");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri).HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs02>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_02")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.Uri, "uri");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri).HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs03>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_03")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs04>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_04")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs05>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_05")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs06>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_06")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs07>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_07")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs08>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_08")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs09>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_09")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs10>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_10")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs11>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_11")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        modelBuilder.Entity<RestLogs12>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rest_logs_12")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey)
                .HasMaxLength(40)
                .HasColumnName("api_key");
            entity.Property(e => e.Authorized)
                .HasMaxLength(1)
                .HasColumnName("authorized");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.Method)
                .HasMaxLength(6)
                .HasColumnName("method");
            entity.Property(e => e.Params)
                .HasColumnType("text")
                .HasColumnName("params");
            entity.Property(e => e.ResponseCode)
                .HasDefaultValueSql("'0'")
                .HasColumnName("response_code");
            entity.Property(e => e.Rtime).HasColumnName("rtime");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Uri)
                .HasMaxLength(255)
                .HasColumnName("uri");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
