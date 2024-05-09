using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class MusteriValidator : AbstractValidator<Cafe_App.Models.Musteri>
	{
		public MusteriValidator()
		{
			// Id için bir kural yok, çünkü genellikle otomatik olarak atanır.

			// Ad (Adı) için doğrulama kuralları
			RuleFor(x => x.Ad)
				.NotNull().WithMessage("Adı boş olamaz!")
				.NotEmpty().WithMessage("Adı boş olamaz!")
				.Length(2, 20).WithMessage("Adı 2 ile 20 karakter arasında olmalıdır!");

			// Soyad (Soyadı) için doğrulama kuralları
			RuleFor(x => x.Soyad)
				.NotNull().WithMessage("Soyadı boş olamaz!")
				.NotEmpty().WithMessage("Soyadı boş olamaz!")
				.Length(2, 20).WithMessage("Soyadı 2 ile 20 karakter arasında olmalıdır!");

			// Eposta (E-posta) için doğrulama kuralları
			RuleFor(x => x.Eposta)
				.NotNull().WithMessage("E-posta boş olamaz!")
				.NotEmpty().WithMessage("E-posta boş olamaz!")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz!");

			// Telefon için doğrulama kuralları
			RuleFor(x => x.Telefon)
				.NotNull().WithMessage("Telefon boş olamaz!")
				.NotEmpty().WithMessage("Telefon boş olamaz!");

			// KayitTarihi için doğrulama kuralları
			RuleFor(x => x.KayitTarihi)
				.NotNull().WithMessage("Kayıt tarihi boş olamaz!");

			// Dogumtarihi için doğrulama kuralları
			RuleFor(x => x.Dogumtarihi)
				.NotNull().WithMessage("Doğum tarihi boş olamaz!");

			// Parola için doğrulama kuralları
			RuleFor(x => x.Parola)
				.NotNull().WithMessage("Parola boş olamaz!")
				.Length(8, 20).WithMessage("Parola 8 ile 20 karakter arasında olmalıdır!")
				.Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$").WithMessage("Parola en az bir büyük harf, bir küçük harf ve bir sayı içermelidir!");

			// Gorunurluk için doğrulama kuralı
			RuleFor(x => x.Gorunurluk)
				.NotNull().WithMessage("Görünürlük durumu belirtilmelidir!");
		}
	}
}
