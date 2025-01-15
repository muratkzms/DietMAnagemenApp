using DietMAnagemenApp.Models;
using FluentValidation;

namespace DietMAnagemenApp.Validation
{
    public class MealValidator : AbstractValidator<Meal>
    {
        public MealValidator()
        {
            RuleFor(meal => meal.Name)
            .NotEmpty().WithMessage("Öğün adı boş olamaz.")
            .Length(3, 100).WithMessage("Öğün adı 3 ile 100 karakter arasında olmalı.");

            RuleFor(meal => meal.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.");

            RuleFor(meal => meal.MealTime)
                .NotEmpty().WithMessage("Öğün saati seçilmelidir.");

            RuleFor(meal => meal.DietPlanId)
                .GreaterThan(0).WithMessage("Geçerli bir Diyet Planı seçmelisiniz.");
        }
    }
}
