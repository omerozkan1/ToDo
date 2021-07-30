using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class DutyUpdateValidator : AbstractValidator<DutyUpdateDto>
    {
        public DutyUpdateValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Ad alanı gereklidir.");
            RuleFor(I => I.UrgencyId).ExclusiveBetween(0, int.MaxValue).WithMessage("Lütfen bir aciliyet durumu seçiniz");
        }
    }
}
