using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class SubcategoryConfig : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {

            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Products).WithOne(x => x.Subcategory).HasForeignKey(x => x.SubcategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.Image).HasDefaultValue("~/Uploads/no-image.png");

        }
    }
}
