using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Kategori
{
    public int Id { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 2, ErrorMessage = "Kategori adı 2 ile 16 karakter arasında olmalıdır.")]
    public string Ad { get; set; }

    public string Tur { get; set; }

    public bool Gorunurluk { get; set; }
}
