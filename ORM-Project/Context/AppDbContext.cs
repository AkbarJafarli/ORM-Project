using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ORM_Project.Models;

namespace ORM_Project.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment>Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AKBAR\\SQLEXPRESS;Database=PB303_ORMProject;Trusted_connection=true;encrypt=false");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
