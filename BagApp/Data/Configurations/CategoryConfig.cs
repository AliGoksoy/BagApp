using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasDefaultValue("~/Uploads/no-image.png");
            builder.Property(x => x.Stat).HasDefaultValue(false);
            builder.HasMany(x => x.Subcategories).WithOne(x => x.Category).HasForeignKey(x => x.CategoryID);
        }
    }
}
