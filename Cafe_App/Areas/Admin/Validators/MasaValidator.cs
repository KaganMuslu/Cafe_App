using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class MasaValidator : AbstractValidator<Masa>
	{
		public MasaValidator()
		{
			// Kod (Masa Kodu) için doğrulama kuralları
			RuleFor(x => x.Kod)
				.NotNull().WithMessage("Masa kodu boş olamaz!")
				.NotEmpty().WithMessage("Masa kodu boş olamaz!");

			// Kapasite için doğrulama kuralları
			RuleFor(x => x.Kapasite)
				.GreaterThanOrEqualTo(1).WithMessage("Kapasite pozitif bir değer olmalıdır!");

			// KategoriId için doğrulama kuralları
			RuleFor(x => x.KategoriId)
				.GreaterThanOrEqualTo(1).WithMessage("Kategori boş olamaz!");
		}
	}
}
