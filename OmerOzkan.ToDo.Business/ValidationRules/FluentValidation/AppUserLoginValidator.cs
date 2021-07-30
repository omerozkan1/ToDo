using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginValidator()
        {
            RuleFor(I => I.Email).NotNull().WithMessage("Email alanı boş geçilemez");
            RuleFor(I => I.Password).NotNull().WithMessage("Parola alanı boş geçilemez");
        }
    }
}
