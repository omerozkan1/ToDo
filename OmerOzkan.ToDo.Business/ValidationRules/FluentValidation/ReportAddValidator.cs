using FluentValidation;
using OmerOzkan.ToDo.Dto.Dtos.ReportDtos;

namespace OmerOzkan.ToDo.Business.ValidationRules.FluentValidation
{
    public class ReportAddValidator : AbstractValidator<ReportAddDto>
    {
        public ReportAddValidator()
        {
            RuleFor(I => I.Description).NotNull().WithMessage("Tanım alanı boş geçilemez");
            RuleFor(I => I.Detail).NotNull().WithMessage("Detay alanı boş geçilemez");
        }
    }
}
