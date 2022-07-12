using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BenchmarkAPI.DAL
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext()
        {
        }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
        }

        public  DbSet<Material> Materials { get; set; } 
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductsMaterialOption> ProductsMaterialOptions { get; set; } = null!;
        public  DbSet<ProductsOffer> ProductsOffers { get; set; } = null!;
        public  DbSet<ProductsSizeOption> ProductsSizeOptions { get; set; } = null!;
        public  DbSet<Unit> Units { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.MaterialId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CreatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UpdatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CreatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UpdatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");
            });

            modelBuilder.Entity<ProductsMaterialOption>(entity =>
            {
                entity.HasKey(e => e.MaterialOptionId)
                    .HasName("PK__Products__F38EA6BE7A0E6956");

                entity.Property(e => e.MaterialOptionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Quentity)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.ProductsMaterialOptions)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK__ProductsM__Mater__719CDDE7");
            });

            modelBuilder.Entity<ProductsOffer>(entity =>
            {
                entity.HasKey(e => e.OfferId)
                    .HasName("PK__Products__8EBCF091269B3B7F");

                entity.Property(e => e.OfferId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CreatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UpdatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");

                entity.HasOne(d => d.MaterialOption)
                    .WithMany(p => p.ProductsOffers)
                    .HasForeignKey(d => d.MaterialOptionId)
                    .HasConstraintName("FK__ProductsO__Mater__7EF6D905");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsOffers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductsO__Produ__00DF2177");

                entity.HasOne(d => d.SizeOption)
                    .WithMany(p => p.ProductsOffers)
                    .HasForeignKey(d => d.SizeOptionId)
                    .HasConstraintName("FK__ProductsO__SizeO__7FEAFD3E");
            });

            modelBuilder.Entity<ProductsSizeOption>(entity =>
            {
                entity.HasKey(e => e.SizeOptionId)
                    .HasName("PK__Products__BE3F6501A1D0973B");

                entity.Property(e => e.SizeOptionId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ProductsSizeOptions)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__ProductsS__UnitI__756D6ECB");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CreatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UpdatedIP")
                    .HasDefaultValueSql("('127.0.0.1')");
            });

        }

    }
}
