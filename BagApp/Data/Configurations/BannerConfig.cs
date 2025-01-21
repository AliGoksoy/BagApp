using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class BannerConfig : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.Property(x => x.Title).HasDefaultValue("Başlık");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasDefaultValue("~/uploads/banner.jpg");

            builder.HasData(new Banner() { Id = 1, Image = "~/uploads/banner.jpg" });

        }
    }
}
