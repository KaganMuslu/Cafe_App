using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class RolValidator : AbstractValidator<Rol>
	{
		public RolValidator()
		{
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Rol adı boş olamaz!")
				.Length(2, 12).WithMessage("Rol adı 2 ile 12 karakter arasında olmalıdır!");
		}
	}
}
