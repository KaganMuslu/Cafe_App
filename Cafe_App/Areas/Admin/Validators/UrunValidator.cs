using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class UrunValidator : AbstractValidator<Urun>
	{
		public UrunValidator()
		{
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Ürün adı boş olamaz!")
				.Length(2, 12).WithMessage("Ürün adı 2 ile 12 karakter arasında olmalıdır!");

			RuleFor(x => x.Aciklama)
				.NotNull().WithMessage("Ürün açıklaması boş olamaz!")
				.Length(2, 40).WithMessage("Ürün açıklaması 2 ile 40 karakter arasında olmalıdır!");

			RuleFor(x => x.Fiyat)
				.NotEmpty().WithMessage("Ürün fiyatı boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Ürün fiyatı pozitif olmalıdır.");

			/*RuleFor(x => x.Fotograf)
				 .NotNull().WithMessage("Fotoğraf boş olmamalıdır.");*/

			RuleFor(x => x.KategoriId)
				.GreaterThanOrEqualTo(1).WithMessage("Ürün kategorisi boş olamaz!");
		}
	}
}
