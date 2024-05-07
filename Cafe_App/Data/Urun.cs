using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Urun
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Ürün adı boş olamaz.")]
    public string Ad { get; set; }

	public string? Aciklama { get; set; }

	[Required(ErrorMessage = "Ürün fiyatı boş olamaz.")]
    public float Fiyat { get; set; }

	public int IndirimYuzdesi { get; set; }

    public float IndirimliFiyat { get; set; }

    public DateOnly IndirimTarihi { get; set; }

    public string? Fotograf { get; set; }

    public bool Akitf { get; set; }

	[Required(ErrorMessage = "Ürün kategorisi boş olamaz.")]
	public int KategoriId { get; set; }

	public bool Gorunurluk { get; set; }

    public Kategori? Kategori { get; set; }
}
