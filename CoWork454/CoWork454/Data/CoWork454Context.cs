using Microsoft.EntityFrameworkCore;
using CoWork454.Models;



namespace CoWork454.Data
{
    public class CoWork454Context : DbContext
    {
        public CoWork454Context(DbContextOptions<CoWork454Context> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //definining many:many relationship between Product and Order via Bookings table
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Bookings)
                .WithOne(b => b.Order)
                .HasForeignKey(b => b.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId);

            //defines composite key
            modelBuilder.Entity<Booking>()
                .HasKey(o => new { o.OrderId, o.ProductId });
                


            //defines one:many relationship between Item and Product
            modelBuilder.Entity<Item>()
                .HasMany(c => c.Products)
                .WithOne(e => e.Item);

            //definining one:many relationship between User and Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserOrders)
                .WithOne(o => o.User);

        }
    }
}

