using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class ReferenceValidator : AbstractValidator<ReferenceDto>
    {
        public ReferenceValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Lütfen referans başlığı ekleyiniz!");
        }

    }
}
