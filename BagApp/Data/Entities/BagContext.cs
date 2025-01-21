using BagApp.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BagApp.Data.Entities
{
    public class BagContext : DbContext
    {
        public BagContext(DbContextOptions<BagContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new BannerConfig());
            modelBuilder.ApplyConfiguration(new ThemeConfig());
            modelBuilder.ApplyConfiguration(new ReferenceConfig());
            modelBuilder.ApplyConfiguration(new QuestionConfig());
            modelBuilder.ApplyConfiguration(new SubcategoryConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
