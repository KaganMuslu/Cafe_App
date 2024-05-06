using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class KategoriValidator : AbstractValidator<Kategori>
	{
		public KategoriValidator()
		{
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Kategori adı boş olamaz!")
				.Length(2, 12).WithMessage("Kategori adı 2 ile 12 karakter arasında olmalıdır!");

			// KategoriId için doğrulama kuralları
			/*RuleFor(x => x.Tur)
				.GreaterThanOrEqualTo(1).WithMessage("Kategori türü boş olamaz!");*/
		}
	}
}
