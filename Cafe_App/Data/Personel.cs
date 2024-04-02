using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Personel
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public string Soyad { get; set; }

    public string Eposta { get; set; }

    public string Telefon { get; set; }

    public decimal Maas { get; set; }

    public DateOnly DogumTarihi { get; set; }

    public DateOnly BaslamaTarihi { get; set; }

    public string Cinsiyet { get; set; }

    public int AdresId { get; set; }

    public int RolId { get; set; }

    public string Parola { get; set; }

    public string Fotograf { get; set; }

    public bool Gorunurluk { get; set; }

	public Adres? Adres { get; set; }

    public ICollection<Bildirim> Bildirimlers { get; set; } = new List<Bildirim>();

    public ICollection<Masa> Masalars { get; set; } = new List<Masa>();

    public Rol? Rol { get; set; }

    public ICollection<Teslimat> Teslimatlars { get; set; } = new List<Teslimat>();
}
