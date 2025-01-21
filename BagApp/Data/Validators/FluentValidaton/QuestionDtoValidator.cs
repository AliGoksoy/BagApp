using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class QuestionDtoValidator : AbstractValidator<QuestionDto>
    {
        public QuestionDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Gerekli ! ");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Gerekli !");


        }
    }
}
