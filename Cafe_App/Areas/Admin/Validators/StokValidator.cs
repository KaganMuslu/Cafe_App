using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class StokValidator : AbstractValidator<Stok>
	{
		public StokValidator()
		{
			RuleFor(x => x.MinStok)
				.NotEmpty().WithMessage("Malzeme min stok boş olmamalıdır.")
				.GreaterThanOrEqualTo(0).WithMessage("Malzeme min stok pozitif olmalıdır.");

			RuleFor(x => x.MaxStok)
				.NotEmpty().WithMessage("Malzeme max stok boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Malzeme max stok pozitif olmalıdır.");
		}
	}
}
