using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject.Models;

public partial class Eyeglasses2023DbContext : DbContext
{
    public Eyeglasses2023DbContext()
    {
    }

    public Eyeglasses2023DbContext(DbContextOptions<Eyeglasses2023DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Eyeglass> Eyeglasses { get; set; }

    public virtual DbSet<LensType> LensTypes { get; set; }

    public virtual DbSet<StoreAccount> StoreAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=1234567890;Database=Eyeglasses2023DB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eyeglass>(entity =>
        {
            entity.HasKey(e => e.EyeglassesId).HasName("PK__Eyeglass__93A83C7B784B5976");

            entity.Property(e => e.EyeglassesId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EyeglassesDescription).HasMaxLength(250);
            entity.Property(e => e.EyeglassesName).HasMaxLength(100);
            entity.Property(e => e.FrameColor).HasMaxLength(50);
            entity.Property(e => e.LensTypeId).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.LensType).WithMany(p => p.Eyeglasses)
                .HasForeignKey(d => d.LensTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Eyeglasse__LensT__29572725");
        });

        modelBuilder.Entity<LensType>(entity =>
        {
            entity.HasKey(e => e.LensTypeId).HasName("PK__LensType__D6DC1FE6F40D9CC1");

            entity.ToTable("LensType");

            entity.Property(e => e.LensTypeId).HasMaxLength(30);
            entity.Property(e => e.LensTypeDescription).HasMaxLength(250);
            entity.Property(e => e.LensTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<StoreAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__StoreAcc__349DA586E2377B02");

            entity.ToTable("StoreAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A1474099E64FF2").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountPassword).HasMaxLength(40);
            entity.Property(e => e.EmailAddress).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
