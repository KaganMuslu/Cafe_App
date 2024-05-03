using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Musteri
{
    public int Id { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 2, ErrorMessage = "Müşteri adı 2 ile 12 karakter arasında olmalıdır.")]
    public string Ad { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 2, ErrorMessage = "Müşteri soyadı 2 ile 12 karakter arasında olmalıdır.")]
    public string Soyad { get; set; }

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com)$", ErrorMessage = "E-posta '@' ve '.com' içermelidir.")]
    public string Eposta { get; set; }

    [Required]
    [Phone]
    [RegularExpression(@"^\d+$", ErrorMessage = "Telefon numarası sadece sayı içermelidir.")]
    public string Telefon { get; set; }

    [Required]
    public DateOnly KayitTarihi { get; set; }

    [Required]
    public DateOnly Dogumtarihi { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Parola en az 8 karakter olmalıdır.")]
    public string Parola { get; set; }

	public bool Gorunurluk { get; set; }

	public ICollection<Adres> Adrelers { get; set; } = new List<Adres>();

	public ICollection<Bildirim> Bildirimlers { get; set; } = new List<Bildirim>();

    public ICollection<KampanyaMusteri> Kampanyalars { get; set; } = new List<KampanyaMusteri>();

    public ICollection<TeslimatAdres> Teslimatadreslers { get; set; } = new List<TeslimatAdres>();

    public ICollection<TeslimatSiparis> Teslimatsiparislers { get; set; } = new List<TeslimatSiparis>();

    public ICollection<Yorum> Yorumlars { get; set; } = new List<Yorum>();
}
