using Cafe_App.Models;
using FluentValidation;

namespace Cafe_App.Areas.Admin.Validators
{
	public class TedarikciValidator : AbstractValidator<Tedarikci>
	{
		public TedarikciValidator()
		{
			// Id için bir kural yok, çünkü genellikle otomatik olarak atanır.

			RuleFor(x => x.AdSoyad)
				.NotNull().WithMessage("Tedarikçi Adı-Soyadı boş olamaz!")
				.NotEmpty().WithMessage("Tedarikçi Adı-Soyadı boş olamaz!")
				.Length(2, 30).WithMessage("Ad-Soyad 2 ile 30 karakter arasında olmalıdır!");

			RuleFor(x => x.Firma)
				.NotNull().WithMessage("Firma adı boş olamaz!")
				.NotEmpty().WithMessage("Firma adı boş olamaz!")
				.Length(2, 30).WithMessage("Firma adı 2 ile 30 karakter arasında olmalıdır!");

			RuleFor(x => x.Eposta)
				.NotNull().WithMessage("E-posta boş olamaz!")
				.NotEmpty().WithMessage("E-posta boş olamaz!")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz!");

			RuleFor(x => x.Adres)
				.NotNull().WithMessage("Doğum tarihi boş olamaz!")
				.Length(2, 40).WithMessage("Adres 2 ile 40 karakter arasında olmalıdır!");

			RuleFor(x => x.Telefon)
				.NotNull().WithMessage("Telefon boş olamaz!")
				.NotEmpty().WithMessage("Telefon boş olamaz!");

			RuleFor(x => x.KayitTarihi)
				.NotNull().WithMessage("Kayıt tarihi boş olamaz!");
		}
	}
}
