using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Malzeme
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Malzeme adı boş olamaz.")]
	[Remote(action: "MalzemeKontrol", controller: "Malzeme", HttpMethod = "POST", AdditionalFields = nameof(Ad) + "," + nameof(Id) )]
    public string Ad { get; set; }

	[Range(0.01, double.MaxValue, ErrorMessage = "Malzeme fiyatı pozitif olmalıdır.")]
	public float Fiyat { get; set; }

	[Required(ErrorMessage = "Tür boş olamaz.")]
	public string Tur { get; set; }

    public bool Gorunurluk { get; set; }

	public Stok? Stok { get; set; }

}
