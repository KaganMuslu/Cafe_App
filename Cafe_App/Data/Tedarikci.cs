using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Tedarikci
{
    public int Id { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 2, ErrorMessage = "Tedarikçi adı 2 ile 16 karakter arasında olmalıdır.")]
    public string AdSoyad { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 2, ErrorMessage = "Firma adı 2 ile 16 karakter arasında olmalıdır.")]
    public string Firma { get; set; }

    [Required]
    [Phone]
    [RegularExpression(@"^\d+$", ErrorMessage = "Telefon numarası sadece sayı içermelidir.")]
    public string Telefon { get; set; }

    [Required]
    public string Adres { get; set; }

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com)$", ErrorMessage = "E-posta '@' ve '.com' içermelidir.")]
    public string Eposta { get; set; }

    public bool Gorunurluk { get; set; }

    public DateOnly KayitTarihi { get; set; }

    public ICollection<StokGirdi> Stokgirdilers { get; set; } = new List<StokGirdi>();

    public ICollection<Stok> Stoklars { get; set; } = new List<Stok>();
}
