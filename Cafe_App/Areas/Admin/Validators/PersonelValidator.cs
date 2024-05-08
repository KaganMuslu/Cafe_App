using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class PersonelValidator : AbstractValidator<Personel>
	{
		public PersonelValidator()
		{
			RuleFor(x => x.Ad)
				.NotEmpty().WithMessage("Personel adı boş olmamalıdır.")
				.Length(2, 20).WithMessage("Personel adı 2 ile 20 karakter arasında olmalıdır!");

			RuleFor(x => x.Soyad)
				.NotEmpty().WithMessage("Personel soyadı boş olmamalıdır.")
				.Length(2, 20).WithMessage("Personel soyadı 2 ile 20 karakter arasında olmalıdır!");

			RuleFor(x => x.Eposta)
				.NotEmpty().WithMessage("E-posta adresi boş olmamalıdır.")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

			RuleFor(x => x.Telefon)
				.NotEmpty().WithMessage("Telefon numarası boş olmamalıdır.")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Telefon numarası geçerli bir formatta olmalıdır.");

			RuleFor(x => x.Maas)
				.NotEmpty().WithMessage("Maaş boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Maaş pozitif olmalıdır.");

			RuleFor(x => x.DogumTarihi)
				.NotEmpty().WithMessage("Doğum tarihi boş olmamalıdır.");

			RuleFor(x => x.BaslamaTarihi)
				.NotEmpty().WithMessage("Başlama tarihi boş olmamalıdır.");

			RuleFor(x => x.Cinsiyet)
				.NotEqual("Cinsiyet").WithMessage("Cinsiyet boş olmamalıdır.");

			RuleFor(x => x.RolId)
				.NotEqual(0).WithMessage("Rol boş olmamalıdır.");

			// Fotograf doğrulaması: Boş olmamalıdır.
			/*RuleFor(x => x.Fotograf)
				 .NotNull().WithMessage("Fotoğraf boş olmamalıdır.");*/

			RuleFor(x => x.Parola)
				.NotEmpty().WithMessage("Parola boş olmamalıdır.")
				.Length(8, 20).WithMessage("Parola 8 ile 20 karakter arasında olmalıdır!")
				.Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$").WithMessage("Parola en az bir büyük harf, bir küçük harf ve bir sayı içermelidir!");
		}
	}
}
