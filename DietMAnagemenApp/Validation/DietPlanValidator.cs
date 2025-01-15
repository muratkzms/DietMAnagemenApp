using DietMAnagemenApp.Models;
using FluentValidation;

namespace DietMAnagemenApp.Validation
{
    public class DietPlanValidator : AbstractValidator<DietPlan>
    {
        public DietPlanValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Diyet planı başlığı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Başlık 100 karakterden uzun olamaz.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");

            RuleFor(x => x.StartAt)
                .NotEmpty().WithMessage("Başlangıç tarihi boş bırakılamaz.")
                .LessThan(x => x.EndAt).WithMessage("Başlangıç tarihi bitiş tarihinden önce olmalıdır.");

            RuleFor(x => x.EndAt)
                .NotEmpty().WithMessage("Bitiş tarihi boş bırakılamaz.")
                .GreaterThan(x => x.StartAt).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

            RuleFor(x => x.StartingWeight)
                .GreaterThan(0).WithMessage("Başlangıç ağırlığı pozitif bir değer olmalıdır.")
                .LessThan(500).WithMessage("Başlangıç ağırlığı 500'den büyük olamaz.");

            RuleFor(x => x.ClientId)
                .NotNull().WithMessage("Diyet planı bir müşteriye atanmalıdır.")
                .GreaterThan(0).WithMessage("Geçerli bir müşteri seçilmelidir.");

            RuleForEach(x => x.Meals)
                .SetValidator(new MealValidator())
                .When(x => x.Meals != null).WithMessage("Öğünler geçerli olmalıdır.");

        }
    }
}
