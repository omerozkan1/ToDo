using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserSignUpValidator : AbstractValidator<AppUserSignUpDto>
    {
        public AppUserSignUpValidator()
        {
            RuleFor(I => I.UserName).NotNull().WithMessage("Kullanıcı Adı boş geçilemez");

            RuleFor(I => I.Password).NotNull().WithMessage("Parola alanı boş geçilemez");
            RuleFor(I => I.ConfirmPassword).NotNull().WithMessage("Parola onay alanı boş geçilemez");
            RuleFor(I => I.ConfirmPassword).Equal(I => I.Password).WithMessage("Parolalarınız eşleşmiyor");
            RuleFor(I => I.Email).NotNull().WithMessage("Email alanı boş geçilemez").EmailAddress().WithMessage("Geçersiz email adresi");

            RuleFor(I => I.Name).NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(I => I.Surname).NotNull().WithMessage("Soyad alanı boş geçilemez");
        }
    }
}
