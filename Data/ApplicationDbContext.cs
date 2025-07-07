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

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<StoreArticle> StoreArticle { get; set; }
        public DbSet<CustomerArticles> CustomerArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreArticle>()
                .HasKey(ta => new { ta.StoreId, ta.ArticleId });

            modelBuilder.Entity<CustomerArticles>()
                .HasKey(ca => new { ca.CustomerId, ca.ArticleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
