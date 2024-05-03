using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Menu
{
    public int Id { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 2, ErrorMessage = "Menü adı 2 ile 16 karakter arasında olmalıdır.")]
    public string Ad { get; set; }

    public string? Aciklama { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Maaş sadece sayı olmalıdır.")]
    public float Fiyat { get; set; }

    public int IndirimYuzdesi { get; set; }

	public float IndirimliFiyat { get; set; }

	public DateOnly IndirimTarihi { get; set; }

    public string? Fotograf { get; set; }

    public bool Akitf { get; set; }

    public int KategoriId { get; set; }

    public bool Gorunurluk { get; set; }

	public Kategori? Kategori { get; set; }
}
