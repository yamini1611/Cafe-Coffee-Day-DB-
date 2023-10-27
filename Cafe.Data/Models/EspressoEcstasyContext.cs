using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data.Models;

public partial class EspressoEcstasyContext : DbContext
{
    public EspressoEcstasyContext()
    {
    }

    public EspressoEcstasyContext(DbContextOptions<EspressoEcstasyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Checkout> Checkouts { get; set; }

    public virtual DbSet<Coffee> Coffees { get; set; }

    public virtual DbSet<Dessert> Desserts { get; set; }

    public virtual DbSet<Eatable> Eatables { get; set; }

    public virtual DbSet<MilkShake> MilkShakes { get; set; }

    public virtual DbSet<Muffin> Muffins { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Cart__C1FFD8610A09B098");

            entity.ToTable("Cart");

            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Product).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__Cart__Userid__49C3F6B7");
        });

        modelBuilder.Entity<Checkout>(entity =>
        {
            entity.HasKey(e => e.Chid).HasName("PK__Checkout__AF05C4E023A15282");

            entity.ToTable("Checkout");

            entity.Property(e => e.Product).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Checkouts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__Checkout__Userid__48CFD27E");
        });

        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.HasKey(e => e.Coffeeid).HasName("PK__Coffee__8EC05FD3E2F64149");

            entity.ToTable("Coffee");

            entity.Property(e => e.CoffeeName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Offer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Dessert>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Desserts__C0312218E6D6BC91");

            entity.Property(e => e.Dname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DName");
            entity.Property(e => e.Offer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Eatable>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__Eatable__C1971B532684385F");

            entity.ToTable("Eatable");

            entity.Property(e => e.Ename)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EName");
            entity.Property(e => e.Offer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<MilkShake>(entity =>
        {
            entity.HasKey(e => e.Mid).HasName("PK__MilkShak__C79638C23B6D3BA7");

            entity.Property(e => e.Mname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MName");
            entity.Property(e => e.Offer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Muffin>(entity =>
        {
            entity.HasKey(e => e.Muid).HasName("PK__Muffins__7E31AB9FBDDE5801");

            entity.Property(e => e.MuName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Offer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__Role__8AF5CA3212BEA554");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__Users__1797D02400741C04");

            entity.HasIndex(e => e.Uname, "UQ__Users__9C5CAF9A9BD05739").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Uname)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK__Users__Roleid__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
