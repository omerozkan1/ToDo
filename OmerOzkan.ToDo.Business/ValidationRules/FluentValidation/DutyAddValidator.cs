using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class DutyAddValidator : AbstractValidator<DutyAddDto>
    {
        public DutyAddValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Ad alanı gereklidir.");
            RuleFor(I => I.UrgencyId).ExclusiveBetween(0, int.MaxValue).WithMessage("Lütfen bir aciliyet durumu seçiniz");
        }
    }
}
