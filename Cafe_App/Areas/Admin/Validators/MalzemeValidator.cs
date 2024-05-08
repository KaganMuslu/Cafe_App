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
				.GreaterThanOrEqualTo(1).WithMessage("Malzeme fiyatı pozitif olmalıdır.");
			
			RuleFor(x => x.Tur)
				.NotNull().WithMessage("Malzeme kategorisi boş olamaz!");
		}
	}
}
