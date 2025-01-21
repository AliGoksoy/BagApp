using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class SubcategoryValidator : AbstractValidator<SubcategoryDto>
    {
        public SubcategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Başlık Gerekli !");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Kategori Gerekli !");
        }
    }
}
