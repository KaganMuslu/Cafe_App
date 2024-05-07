using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class RezervasyonValidator : AbstractValidator<Rezervasyon>
	{
		public RezervasyonValidator()
		{
			RuleFor(x => x.KisiSayisi)
				.NotEmpty().WithMessage("Kişi sayısı boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Kişi sayısı pozitif olmalıdır.");

			RuleFor(x => x.BaslangicSaat)
				.NotEmpty().WithMessage("Rezervasyon başlangıç saati boş olmamalıdır.");

			RuleFor(x => x.BitisSaat)
				.NotEmpty().WithMessage("Rezervasyon bitiş saati boş olmamalıdır.")
				.GreaterThan(x => x.BaslangicSaat).WithMessage("Bitiş zamanı başlangıç saatten sonra olmalıdır.");


			RuleFor(x => x.Tarih)
				.NotEmpty().WithMessage("Rezervasyon tarihi boş olmamalıdır.");

			RuleFor(x => x.Talep)
				.MaximumLength(30).WithMessage("Rezervasyon talebi 30 karakterden az olmalıdır.");
		}
	}
}
