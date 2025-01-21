using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class ReferenceConfig : IEntityTypeConfiguration<Reference>
    {
        public void Configure(EntityTypeBuilder<Reference> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasDefaultValue("~/uploads/banner.jpg");
        }
    }
}
