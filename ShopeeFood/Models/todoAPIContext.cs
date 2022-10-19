using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopeeFood.Models
{
    public partial class todoAPIContext : DbContext
    {
        public todoAPIContext()
        {
        }

        public todoAPIContext(DbContextOptions<todoAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<TodoItem> TodoItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=todoAPI;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.UserId, "IX_orders_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId);

                entity.HasMany(d => d.Products)
                    .WithMany(p => p.Orders)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrdersProduct",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                        r => r.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                        j =>
                        {
                            j.HasKey("OrderId", "ProductId");

                            j.ToTable("ordersProduct");

                            j.HasIndex(new[] { "ProductId" }, "IX_ordersProduct_ProductId");
                        });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Password).HasDefaultValueSql("(N'')");

                entity.Property(e => e.RefreshToken).HasDefaultValueSql("(N'')");

                entity.Property(e => e.RefreshTokenExpiryTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
