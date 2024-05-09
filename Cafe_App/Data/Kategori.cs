using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Kategori
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Kategori adı boş olamaz.")]
	[Remote(action: "KategoriKontrol", controller: "Kategori", HttpMethod = "POST", AdditionalFields = nameof(Ad))]
    public string Ad { get; set; }

	[Required(ErrorMessage = "Kategori türü boş olamaz.")]
    public string Tur { get; set; }

	public bool Gorunurluk { get; set; }
}
