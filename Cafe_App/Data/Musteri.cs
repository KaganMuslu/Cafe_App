using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Musteri
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Müşteri adı boş olmamalıdır.")]
    public string Ad { get; set; }

	[Required(ErrorMessage = "Müşteri soyadı boş olmamalıdır.")]
	public string Soyad { get; set; }

	[Required(ErrorMessage = "Müşteri e-postası boş olmamalıdır.")]
	[Remote(action: "MusteriKontrol", controller: "Musteri", HttpMethod = "POST", AdditionalFields = nameof(Eposta) + "," + nameof(Id) )]
	public string Eposta { get; set; }

	[Required(ErrorMessage = "Müşteri telefon numarası boş olmamalıdır.")]
	[Remote(action: "MusteriKontrol", controller: "Musteri", HttpMethod = "POST", AdditionalFields = nameof(Telefon) + "," + nameof(Id) )]
	public string Telefon { get; set; }

	public DateOnly KayitTarihi { get; set; }

	[Remote(action: "MusteriKontrol", controller: "Musteri", HttpMethod = "POST", AdditionalFields = nameof(Dogumtarihi))]
    public DateOnly Dogumtarihi { get; set; }

	[Required(ErrorMessage = "Müşteri parolası boş olmamalıdır.")]
    public string Parola { get; set; }

	public bool Gorunurluk { get; set; }

	public ICollection<Adres> Adrelers { get; set; } = new List<Adres>();

	public ICollection<Bildirim> Bildirimlers { get; set; } = new List<Bildirim>();

    public ICollection<KampanyaMusteri> Kampanyalars { get; set; } = new List<KampanyaMusteri>();

    public ICollection<TeslimatAdres> Teslimatadreslers { get; set; } = new List<TeslimatAdres>();

    public ICollection<TeslimatSiparis> Teslimatsiparislers { get; set; } = new List<TeslimatSiparis>();

    public ICollection<Yorum> Yorumlars { get; set; } = new List<Yorum>();
}
