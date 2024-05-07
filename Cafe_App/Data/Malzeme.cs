using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Malzeme
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Malzeme adı boş olamaz.")]
    public string Ad { get; set; }

	[Required(ErrorMessage = "Malzeme kategorisi boş olamaz.")]
	public string Tur { get; set; }

	public float Fiyat { get; set; }

	public bool Gorunurluk { get; set; }

	public Stok? Stok { get; set; }
}
