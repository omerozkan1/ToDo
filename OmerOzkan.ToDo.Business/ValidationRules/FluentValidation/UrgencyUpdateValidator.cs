using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class UrgencyUpdateValidator : AbstractValidator<UrgencyUpdateDto>
    {
        public UrgencyUpdateValidator()
        {
            RuleFor(I => I.Description).NotNull().WithMessage("Tanım alanı boş geçilemez.");
        }
    }
}
