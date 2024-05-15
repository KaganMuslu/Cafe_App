using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Menu
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Menü adı boş olamaz.")]
	[Remote(action: "MenuKontrol", controller: "Menu", HttpMethod = "POST", AdditionalFields = nameof(Ad) + "," + nameof(Id) )]
    public string Ad { get; set; }

	public string? Aciklama { get; set; }

    public float Fiyat { get; set; }

	public DateOnly IndirimTarihi { get; set; }

    public string? Fotograf { get; set; }

    public bool Akitf { get; set; }

	[Required(ErrorMessage = "Menü kategorisi boş olamaz.")]
    public int KategoriId { get; set; }

	public bool Gorunurluk { get; set; }

	public Kategori? Kategori { get; set; }
}
