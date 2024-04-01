using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Masa
{
    public int Id { get; set; }

    public string Kod { get; set; }

    public bool Durum { get; set; }

    public int Kapasite { get; set; }

    public bool Temizlik { get; set; }

    public int Tutar { get; set; }

    public int OdenenTutar { get; set; }

    public int PersonelId { get; set; }

    public ICollection<Musteri> Musterilers { get; set; } = new List<Musteri>();

    public Personel? Personel { get; set; }
}
