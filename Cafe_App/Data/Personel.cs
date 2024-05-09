using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Personel
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Personel adı boş olmamalıdır.")]
	public string Ad { get; set; }

	[Required(ErrorMessage = "Personel soyadı boş olmamalıdır.")]
    public string Soyad { get; set; }

	[Required(ErrorMessage = "E-posta adresi boş olmamalıdır.")]
	[Remote(action: "PersonelKontrol", controller: "Personel", HttpMethod = "POST", AdditionalFields = nameof(Eposta))]
	public string Eposta { get; set; }

	[Required(ErrorMessage = "Telefon numarası boş olmamalıdır.")]
	[Remote(action: "PersonelKontrol", controller: "Personel", HttpMethod = "POST", AdditionalFields = nameof(Telefon))]
	public string Telefon { get; set; }

	[Required(ErrorMessage = "Maaş boş olmamalıdır.")]

	public float Maas { get; set; }

	[Required(ErrorMessage = "Doğum tarihi boş olmamalıdır.")]
	[Remote(action: "PersonelKontrol", controller: "Personel", HttpMethod = "POST", AdditionalFields = nameof(DogumTarihi))]
	public DateOnly DogumTarihi { get; set; }

	[Required(ErrorMessage = "Başlama tarihi boş olmamalıdır.")]
	public DateOnly BaslamaTarihi { get; set; }

	[Required(ErrorMessage = "Cinsiyet boş olmamalıdır.")]
	public string Cinsiyet { get; set; }

	[Required(ErrorMessage = "Adres boş olmamalıdır.")]
	public string Adres { get; set; }

	[Required(ErrorMessage = "Parola boş olmamalıdır.")]
	public string Parola { get; set; }

	public string? Fotograf { get; set; }

    public bool Gorunurluk { get; set; }

    public ICollection<Bildirim> Bildirimlers { get; set; } = new List<Bildirim>();

    public ICollection<Masa> Masalars { get; set; } = new List<Masa>();

	[Required(ErrorMessage = "Rol boş olmamalıdır.")]
    public int RolId { get; set; }

	public Rol? Rol { get; set; }

    public ICollection<Teslimat> Teslimatlars { get; set; } = new List<Teslimat>();
}
