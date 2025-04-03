﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.DBContext;

public partial class Sp25CosmeticsDBContext : DbContext
{
    public Sp25CosmeticsDBContext()
    {
    }

    public Sp25CosmeticsDBContext(DbContextOptions<Sp25CosmeticsDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CosmeticCategory> CosmeticCategories { get; set; }

    public virtual DbSet<CosmeticInformation> CosmeticInformations { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CosmeticCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Cosmetic__19093A2B6726B62F");

            entity.ToTable("CosmeticCategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(120);
            entity.Property(e => e.FormulationType)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.UsagePurpose)
                .IsRequired()
                .HasMaxLength(250);
        });

        modelBuilder.Entity<CosmeticInformation>(entity =>
        {
            entity.HasKey(e => e.CosmeticId).HasName("PK__Cosmetic__98ED527E31152935");

            entity.ToTable("CosmeticInformation");

            entity.Property(e => e.CosmeticId)
                .HasMaxLength(30)
                .HasColumnName("CosmeticID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CosmeticName)
                .IsRequired()
                .HasMaxLength(160);
            entity.Property(e => e.CosmeticSize)
                .IsRequired()
                .HasMaxLength(400);
            entity.Property(e => e.DollarPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ExpirationDate)
                .IsRequired()
                .HasMaxLength(160);
            entity.Property(e => e.SkinType)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.CosmeticInformations)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CosmeticI__Categ__3C69FB99");
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA586F6473AD5");

            entity.ToTable("SystemAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__SystemAc__49A1474076CA251C").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountNote)
                .IsRequired()
                .HasMaxLength(240);
            entity.Property(e => e.AccountPassword)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}