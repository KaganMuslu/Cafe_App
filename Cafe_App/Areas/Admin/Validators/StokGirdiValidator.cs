using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class StokGirdiValidator : AbstractValidator<StokGirdi>
	{
		public StokGirdiValidator()
		{
			RuleFor(x => x.MalzemeId)
				.NotNull().WithMessage("Girdi malzemesi boş olamaz!");

			RuleFor(x => x.TedarikciId)
				.NotNull().WithMessage("Tedarikçi firma boş olamaz!");

			RuleFor(x => x.Miktar)
				.NotNull().WithMessage("Stok miktarı boş olamaz!")
				.GreaterThanOrEqualTo(1).WithMessage("Stok miktarı pozitif olmalıdır.");

			RuleFor(x => x.AlısFiyati)
				.NotNull().WithMessage("Stok alış fiyatı boş olamaz!")
				.GreaterThanOrEqualTo(1).WithMessage("Stok alış fiyatı pozitif olmalıdır.");
		}
	}
}
