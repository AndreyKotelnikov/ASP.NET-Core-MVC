using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Product> Products { get; set; }
        
        public WebStoreDbContext(DbContextOptions<WebStoreDbContext> options) : base(options) { }
    }
}
