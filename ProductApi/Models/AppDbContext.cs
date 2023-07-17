using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Code = "1001", Name = "SamsungTV", Quantity = 10, Price = 10000, Description = "LED TV", Image = "", Category = 1, SubCategory = 1 });

            modelBuilder.Entity<Product>().HasData(
               new Product() { Id = 2, Code = "1002", Name = "XiaomiMobile", Quantity = 5, Price = 5000, Description = "Mobile", Image = "", Category = 1, SubCategory = 2 });

            modelBuilder.Entity<Product>().HasData(
               new Product() { Id = 3, Code = "1003", Name = "Walkmate", Quantity = 11, Price = 500, Description = "Slippers", Image = "", Category = 3, SubCategory = 6 });

        }
    }
}
