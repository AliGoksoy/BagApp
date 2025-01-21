using BagApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagApp.Data.Configurations
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {

            builder.HasKey(x => x.Id);
        }
    }
}
