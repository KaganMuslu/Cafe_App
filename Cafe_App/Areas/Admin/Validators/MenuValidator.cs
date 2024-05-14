using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class MenuValidator : AbstractValidator<Menu>
	{
		public MenuValidator()
		{
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Menü adı boş olamaz!")
				.Length(2, 30).WithMessage("Menü adı 2 ile 30 karakter arasında olmalıdır!");

			RuleFor(x => x.Aciklama)
				.NotNull().WithMessage("Menü açıklaması boş olamaz!")
				.Length(2, 40).WithMessage("Menü açıklaması 2 ile 40 karakter arasında olmalıdır!");

			RuleFor(x => x.Fiyat)
				.NotEmpty().WithMessage("Menü fiyatı boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Menü fiyatı pozitif olmalıdır.");

			RuleFor(x => x.IndirimliFiyat)
				.GreaterThanOrEqualTo(1).WithMessage("Menü fiyatı pozitif olmalıdır.");

			RuleFor(x => x.IndirimYuzdesi)
				.GreaterThanOrEqualTo(1).WithMessage("Menü fiyatı pozitif olmalıdır.");

			/*RuleFor(x => x.Fotograf)
				 .NotNull().WithMessage("Fotoğraf boş olmamalıdır.");*/

			RuleFor(x => x.KategoriId)
				.GreaterThanOrEqualTo(1).WithMessage("Menü kategorisi boş olamaz!");
		}
	}
}
