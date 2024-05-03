using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Malzeme
{
    public int Id { get; set; }

    [Required]
    public string Ad { get; set; }

    public string Tur { get; set; }

    public float Fiyat { get; set; }

	public bool Gorunurluk { get; set; }

	public Stok? Stok { get; set; }
}
