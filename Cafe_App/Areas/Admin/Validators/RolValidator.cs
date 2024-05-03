using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class RolValidator : AbstractValidator<Rol>
	{
		public RolValidator()
		{
			RuleFor(x => x.Ad).Length(2, 12).WithMessage("Rol 2 ile 12 karakter arasında olmalıdır!");
		}
	}
}
