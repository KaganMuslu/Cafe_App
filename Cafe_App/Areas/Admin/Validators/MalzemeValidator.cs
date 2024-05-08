using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class MalzemeValidator : AbstractValidator<Malzeme>
	{
		public MalzemeValidator()
		{
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Malzeme adı boş olamaz!")
				.Length(2, 30).WithMessage("Malzeme adı 2 ile 30 karakter arasında olmalıdır!");

			RuleFor(x => x.Fiyat)
				.NotEmpty().WithMessage("Malzeme fiyatı boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Malzeme fiyatı pozitif olmalıdır.")
				.Must(fiyat => (fiyat * 100) % 1 == 0).WithMessage("Lütfen 0.01'in katı olan bir değer girin.");

			RuleFor(x => x.Tur)
				.NotNull().WithMessage("Malzeme kategorisi boş olamaz!");
		}
	}
}
