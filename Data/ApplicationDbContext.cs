using API_Shop_Online.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Shop_Online.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<StoreArticle> StoreArticles { get; set; }
        public DbSet<CustomerArticle> CustomerArticles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleArticle> SaleArticles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                        .Property(a => a.Price)
                        .HasPrecision(18, 2);
            modelBuilder.Entity<StoreArticle>()
                .HasKey(ta => new { ta.StoreId, ta.ArticleId });

            modelBuilder.Entity<CustomerArticle>()
                .HasKey(ca => new { ca.CustomerId, ca.ArticleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
