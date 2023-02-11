using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.Models;

public partial class Gr692BvvContext : DbContext
{
    public Gr692BvvContext()
    {
    }

    public Gr692BvvContext(DbContextOptions<Gr692BvvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<TypesPurchase> TypesPurchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=10.30.0.137;Port=5432;Database=gr692_bvv;Username=gr692_bvv;Password=300 bucks");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Fio)
                .HasMaxLength(150)
                .HasColumnName("fio");
            entity.Property(e => e.Passport)
                .HasMaxLength(15)
                .HasColumnName("passport");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.ManufCountry)
                .HasMaxLength(50)
                .HasColumnName("manuf_country");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("purchases_pkey");

            entity.ToTable("purchases");

            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.ClientIdFk).HasColumnName("client_id_fk");
            entity.Property(e => e.DatePurchase).HasColumnName("date_purchase");
            entity.Property(e => e.ProductIdFk).HasColumnName("product_id_fk");
            entity.Property(e => e.TypePurchaseIdFk).HasColumnName("type_purchase_id_fk");

            entity.HasOne(d => d.ClientIdFkNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ClientIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_client_id_fk_fkey");

            entity.HasOne(d => d.ProductIdFkNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ProductIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_product_id_fk_fkey");

            entity.HasOne(d => d.TypePurchaseIdFkNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.TypePurchaseIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchases_type_purchase_id_fk_fkey");
        });

        modelBuilder.Entity<TypesPurchase>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("types_purchase_pkey");

            entity.ToTable("types_purchase");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.CashTransfer).HasColumnName("cash_transfer");
            entity.Property(e => e.CreditNow).HasColumnName("credit_now");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
