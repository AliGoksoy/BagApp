using BagApp.Data.Dtos;
using FluentValidation;

namespace BagApp.Data.Validators.FluentValidaton
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Gerekli");
        }
    }
}
