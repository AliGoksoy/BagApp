using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Gereklidir");

        }
    }

    public class CategoryCreateValidator : AbstractValidator<CreateCategoryDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Gereklidir");

        }
    }

    public class CategoryUpdateValidator : AbstractValidator<UpdateCategoryDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adı Gereklidir");

        }
    }
}
