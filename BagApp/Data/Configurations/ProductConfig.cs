using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description);
            builder.Property(x => x.Home).HasDefaultValue(false);
            builder.Property(x => x.Stat).HasDefaultValue(false);
            builder.Property(x => x.Image).HasDefaultValue("~/Uploads/no-image.png");
            builder.HasMany(x => x.ProductMedias).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Subcategory).WithMany(x => x.Products).HasForeignKey(x => x.SubcategoryId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
