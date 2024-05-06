using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class PersonelValidator : AbstractValidator<Personel>
	{
		public PersonelValidator()
		{
			// Ad doğrulaması: Uzunluk 2 ila 12 karakter arasında olmalıdır.
			RuleFor(x => x.Ad)
				.NotEmpty().WithMessage("Personel adı boş olmamalıdır.")
				.Length(2, 18).WithMessage("Personel adı 2 ile 18 karakter arasında olmalıdır!");

			// Soyad doğrulaması: Uzunluk 2 ila 12 karakter arasında olmalıdır.
			RuleFor(x => x.Soyad)
				.NotEmpty().WithMessage("Personel soyadı boş olmamalıdır.")
				.Length(2, 12).WithMessage("Personel soyadı 2 ile 12 karakter arasında olmalıdır!");

			// E-posta doğrulaması: Geçerli bir e-posta adresi olmalıdır.
			RuleFor(x => x.Eposta)
				.NotEmpty().WithMessage("E-posta adresi boş olmamalıdır.")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

			// Telefon doğrulaması: Sadece rakam içermeli ve geçerli bir telefon numarası olmalıdır.
			RuleFor(x => x.Telefon)
				.NotEmpty().WithMessage("Telefon numarası boş olmamalıdır.")
				.Matches(@"^\d+$").WithMessage("Telefon numarası sadece sayı içermelidir.")
				.Matches(@"^\+?[0-9]*$").WithMessage("Telefon numarası geçerli bir formatta olmalıdır.");

			// Maaş doğrulaması: Sadece sayı olmalıdır.
			RuleFor(x => x.Maas)
				.NotEmpty().WithMessage("Maaş boş olmamalıdır.")
				.GreaterThanOrEqualTo(1).WithMessage("Maaş pozitif olmalıdır.");

			// Doğum Tarihi doğrulaması: Boş olmamalı ve geçerli bir tarih olmalıdır.
			RuleFor(x => x.DogumTarihi)
				.NotEmpty().WithMessage("Doğum tarihi boş olmamalıdır.");

			// Başlama Tarihi doğrulaması: Boş olmamalı ve geçerli bir tarih olmalıdır.
			RuleFor(x => x.BaslamaTarihi)
				.NotEmpty().WithMessage("Başlama tarihi boş olmamalıdır.");

			// Cinsiyet doğrulaması: Boş olmamalıdır.
			RuleFor(x => x.Cinsiyet)
				.NotEqual("Cinsiyet").WithMessage("Cinsiyet boş olmamalıdır.");

			// Rol doğrulaması: Boş olmamalı.
			RuleFor(x => x.RolId)
				.NotEqual(0).WithMessage("Rol boş olmamalıdır.");

			// Fotograf doğrulaması: Boş olmamalıdır.
			/*RuleFor(x => x.Fotograf)
				 .NotNull().WithMessage("Fotoğraf boş olmamalıdır.");*/

			// Adres doğrulaması: Boş olmamalıdır.
			RuleFor(x => x.Adres)
				.NotEmpty().WithMessage("Adres boş olmamalıdır.");

			// Parola doğrulaması: En az 8 karakter uzunluğunda olmalıdır.
			RuleFor(x => x.Parola)
				.NotEmpty().WithMessage("Parola boş olmamalıdır.")
				.MinimumLength(8).WithMessage("Parola en az 8 karakter olmalıdır.");
		}
	}
}
