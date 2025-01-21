using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı gerekli");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori gerekli");
            RuleFor(x => x.SubcategoryId).NotEmpty().WithMessage("Alt Kategori gerekli");
        }
    }
}
