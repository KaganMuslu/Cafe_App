using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Kategori
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Kategori adı boş olamaz.")]
    public string Ad { get; set; }

    public string Tur { get; set; }

    public bool Gorunurluk { get; set; }
}
