using Entities;
using System.Data.Entity;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        // Yapıcı metod ile bağlantı cümlesi (web.config içindeki "name=...")
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        // DbSet tanımları
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Fluent API ayarları (gerekirse)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Örnek: İlişki ayarı
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
