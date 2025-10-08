using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public ShopDbContext()
        {
            //Database.EnsureCreated();
        }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = default!; // (numbers - 0, bool - false, class - null)
        public DbSet<OrderDetails> OrderDetails { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("workstation id=shop-db.mssql.somee.com;packet size=4096;user id=vladnaz_SQLLogin_1;pwd=478semocox;data source=shop-db.mssql.somee.com;persist security info=False;initial catalog=shop-db;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DbInitializer.SeedCategories(modelBuilder);
            //DbInitializer.SeedProducts(modelBuilder);

            modelBuilder.SeedCategories();
            modelBuilder.SeedProducts();

            // TODO: move to separate class
            modelBuilder.Entity<OrderDetails>().HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<OrderDetails>().HasOne(x => x.Product).WithMany(x => x.Orders).HasForeignKey(x => x.ProductId);
        }
    }
}
