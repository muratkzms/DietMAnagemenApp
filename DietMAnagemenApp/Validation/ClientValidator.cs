using DietMAnagemenApp.Models;
using FluentValidation;

namespace DietMAnagemenApp.Validation
{
    public class ClientValidator:AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("Ad soyad boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ad soyad 100 karakterden uzun olamaz.");

            RuleFor(x => x.Picture)
                .NotEmpty().WithMessage("Profil fotoğrafı boş bırakılamaz.")
                .Matches(@"\.(jpg|jpeg|png|gif)$").WithMessage("Profil fotoğrafı yalnızca jpg, jpeg, png veya gif formatında olmalıdır.");

            RuleFor(x => x.About)
                .MaximumLength(500).WithMessage("Hakkında bölümü 500 karakterden uzun olamaz.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Kilo pozitif bir değer olmalıdır.")
                .LessThan(500).WithMessage("Kilo 500'den büyük olamaz.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Boy pozitif bir değer olmalıdır.")
                .LessThan(300).WithMessage("Boy 300 cm'den büyük olamaz.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.")
                .Must(BeAValidDate).WithMessage("Geçerli bir doğum tarihi giriniz.")
                .LessThan(DateTime.Today).WithMessage("Doğum tarihi bugünden büyük olamaz.");

        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default(DateTime);
        }
    }
}
