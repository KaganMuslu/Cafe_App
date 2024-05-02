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
    public string Ad { get; set; }

    [Required]
    public string Soyad { get; set; }

    [Required]
    [EmailAddress]
    public string Eposta { get; set; }

    [Required]
    [Phone]
    public string Telefon { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Maas 0'dan büyük olmalıdır.")]
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
    [MinLength(8)]
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
