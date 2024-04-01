using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Adres
{
    public int Id { get; set; }

    public string Il { get; set; }

    public string Ilce { get; set; }

    public string Koy { get; set; }

    public string Mahalle { get; set; }

    public string Sokak { get; set; }

    public int No { get; set; }

    public string? Tarif { get; set; }

    public ICollection<Musteri> Musterilers { get; set; } = new List<Musteri>();

    public ICollection<Personel> Personellers { get; set; } = new List<Personel>();

    public ICollection<Tedarikci> Tedarikcilers { get; set; } = new List<Tedarikci>();

    public ICollection<TeslimatAdres> Teslimatadreslers { get; set; } = new List<TeslimatAdres>();
}
