
using EducationApp.Dtos;
using FluentValidation;

namespace EducationApp.ValidationRules
{
    public class UserValidationRules : AbstractValidator<UserRegisterDto>
    {

        public UserValidationRules()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Ad Alanını Boş Geçmeyiniz");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail Alanını Boş Geçmeyiniz");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanını Boş Geçmeyiniz");

            RuleFor(x=>x.ConfirmPassword).Equal(y=>y.Password).WithMessage("Parolalarınız eşleşmiyor");
        }




    }
}
