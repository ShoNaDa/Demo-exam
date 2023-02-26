using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.Models;

public partial class Gr692BvvContext : DbContext
{
    public Gr692BvvContext()
    {}
    
    public Gr692BvvContext(DbContextOptions<Gr692BvvContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }
    
    public virtual DbSet<TypesPurchase> TypesPurchases { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=gr692_bvv;Username=gr692_bvv;Password=300 bucks");
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.ManufCountry)
                .HasMaxLength(255)
                .HasColumnName("manufCountry");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasColumnName("model");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("purchase_pkey");

            entity.ToTable("purchases");

            entity.Property(e => e.PurchaseId).HasColumnName("purchaseId");
            entity.Property(e => e.ProductIdFk).HasColumnName("productIdFk");
            entity.Property(e => e.ClientIdFk).HasColumnName("clientIdFk");
            entity.Property(e => e.DatePurchase).HasColumnName("datePurchase");
            entity.Property(e => e.TypePurchaseIdFk)
                .HasMaxLength(255)
                .HasColumnName("typePurchaseIdFk");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ProductIdFk)
                .HasConstraintName("purchases_productIdFk_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ClientIdFk)
                .HasConstraintName("payments_clientIdFk_fkey");
        });
        
        modelBuilder.Entity<TypesPurchase>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("typePurchase_pkey");

            entity.ToTable("typePurchases");

            entity.Property(e => e.TypeId).HasColumnName("purchaseId");
            entity.Property(e => e.CashTransfer).HasColumnName("productIdFk");
            entity.Property(e => e.CreditNow).HasColumnName("clientIdFk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}