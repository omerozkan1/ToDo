using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class UrgencyAddValidator : AbstractValidator<UrgencyAddDto>
    {
        public UrgencyAddValidator()
        {
            RuleFor(I => I.Description).NotNull().WithMessage("Tanım alanı boş geçilemez.");
        }
    }
}
