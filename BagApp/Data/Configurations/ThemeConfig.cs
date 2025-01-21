using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class ThemeConfig : IEntityTypeConfiguration<ThemeSetting>
    {
        public void Configure(EntityTypeBuilder<ThemeSetting> builder)
        {
            builder.Property(x => x.CompanyName).HasDefaultValue("Firma Adı");
            builder.Property(x => x.LogoUrl).HasDefaultValue("~/uploads/logo.png");
            builder.Property(x => x.FooterLogoUrl).HasDefaultValue("~/uploads/logo.png");
            builder.Property(x => x.Gsm).HasDefaultValue("+90 (530) 123 0000");
            builder.Property(x => x.Phone).HasDefaultValue("+90 (530) 123 0000");
            builder.Property(x => x.About).HasDefaultValue("Firma hakkında kısa bilgilendirme metni");
            builder.Property(x => x.ShortAbout).HasDefaultValue("Firma Footer Bilgisi");
            builder.Property(x => x.Address).HasDefaultValue("Adres Bilgisi");
            builder.Property(x => x.Favicon).HasDefaultValue("~/uploads/favicon.png");
            builder.Property(x => x.Facebook).HasDefaultValue("#");
            builder.Property(x => x.Youtube).HasDefaultValue("#");
            builder.Property(x => x.Twitter).HasDefaultValue("#");

            builder.HasData(new ThemeSetting()
            {

                Id = 1,
                CompanyName = "Firma Adı",
                LogoUrl = "~/uploads/logo.png",
                FooterLogoUrl = "~/uploads/logo.png",
                Gsm = "+90 (530) 123 0000",
                Phone = "+90 (530) 123 0000",
                Email = "#",
                Email2 = "#",
                SiteUrl = "Firma Ünvanı",
                About = "Firma hakkında kısa bilgilendirme metni",
                ShortAbout = "Firma Footer Bilgisi",
                Address = "Adres Bilgisi",
                Favicon = "~/uploads/favicon.png",
                Facebook = "#",
                Youtube = "#",
                Twitter = "#",
                GoogleVerify = "#",

            });


        }
    }
}
