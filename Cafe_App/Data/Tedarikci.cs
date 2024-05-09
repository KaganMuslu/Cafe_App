using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Tedarikci
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Ad-Soyad boş olamaz.")]
    public string AdSoyad { get; set; }

	[Required(ErrorMessage = "Tedarikçi firma boş olamaz.")]
	[Remote(action: "TedarikciKontrol", controller: "Tedarikci", HttpMethod = "POST", AdditionalFields = nameof(Firma))]
	public string Firma { get; set; }

	[Required(ErrorMessage = "Telefon numarası boş olamaz.")]
	[Remote(action: "TedarikciKontrol", controller: "Tedarikci", HttpMethod = "POST", AdditionalFields = nameof(Telefon))]
	public string Telefon { get; set; }

	[Required(ErrorMessage = "Adres boş olamaz.")]
	public string Adres { get; set; }

	[Required(ErrorMessage = "E-Posta boş olamaz.")]
	[Remote(action: "TedarikciKontrol", controller: "Tedarikci", HttpMethod = "POST", AdditionalFields = nameof(Eposta))]
	public string Eposta { get; set; }

	public bool Gorunurluk { get; set; }

    public DateOnly KayitTarihi { get; set; }

    public ICollection<StokGirdi> Stokgirdilers { get; set; } = new List<StokGirdi>();

    public ICollection<Stok> Stoklars { get; set; } = new List<Stok>();
}
