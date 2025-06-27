using DeliveryInternation2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryInternation2._0.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishInCart> DishesInCart { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<StorageUserToken> StorageUsersTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .Property(o => o.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<StorageUserToken>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
