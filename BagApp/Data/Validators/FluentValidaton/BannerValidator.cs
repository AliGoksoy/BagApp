using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class BannerValidator : AbstractValidator<BannerDto>
    {
        public BannerValidator()
        {
            RuleFor(b => b.LinkUrl).NotEmpty().WithMessage("Link eklenmedi. Bunun yerine '#' kullanabilirsiniz");
        }
    }
}
