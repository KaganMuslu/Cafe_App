using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Urun
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public string Aciklama { get; set; }

    public string Detay { get; set; }

    public decimal Fiyat { get; set; }

    public string Fotograf { get; set; }

    public bool Akitf { get; set; }

    public int IndirimliFiyat { get; set; }

    public int KategoriId { get; set; }

	public bool Gorunurluk { get; set; }

    public Kategori? Kategori { get; set; }
}
