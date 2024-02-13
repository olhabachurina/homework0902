using homework0902.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902
{
    public class OnlineStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.RowVersion).IsRowVersion();
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(500);
                entity.HasMany(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId);
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.PriceAtOrder).HasColumnType("decimal(18,2)");
                entity.HasOne(oi => oi.Product)
                      .WithMany()
                      .HasForeignKey(oi => oi.ProductId);
            });
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop.", Price = 1200.00m, Quantity = 10 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone.", Price = 800.00m, Quantity = 15 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = "Cust001", OrderDate = new DateTime(2023, 1, 1), TotalPrice = 2000.00m, Status = "Новый", ShippingAddress = "123 Main St, Anytown, AN" }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, PriceAtOrder = 1200.00m },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, PriceAtOrder = 800.00m }
            );
        }
    }
}
        