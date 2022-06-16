using CSITCommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CSITCommerce
{
    public class CommerceDbContext : IdentityDbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryModel>().ToTable("Category");
            builder.Entity<ProductModel>().ToTable("Product");

            base.OnModelCreating(builder);
        }
    }
}
