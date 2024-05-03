using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Personel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 2, ErrorMessage = "Personek adı 2 ile 12 karakter arasında olmalıdır.")]
    public string Ad { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 2, ErrorMessage = "Personel soyadı 2 ile 12 karakter arasında olmalıdır.")]
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
    [Range(0, double.MaxValue, ErrorMessage = "Maaş sadece sayı olmalıdır.")]
    public float Maas { get; set; }

    [Required]
    public DateOnly DogumTarihi { get; set; }

    [Required]
    public DateOnly BaslamaTarihi { get; set; }

    [Required]
    public string Cinsiyet { get; set; }

    [Required]
    public string Adres { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Parola en az 8 karakter olmalıdır.")]
    public string Parola { get; set; }

    public string? Fotograf { get; set; }

    public bool Gorunurluk { get; set; }

    public ICollection<Bildirim> Bildirimlers { get; set; } = new List<Bildirim>();

    public ICollection<Masa> Masalars { get; set; } = new List<Masa>();

    [Required]
    public int RolId { get; set; }

    public Rol? Rol { get; set; }

    public ICollection<Teslimat> Teslimatlars { get; set; } = new List<Teslimat>();
}
