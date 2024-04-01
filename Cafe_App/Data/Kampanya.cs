using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Kampanya
{
    public int Id { get; set; }

    public string Kod { get; set; }

    public int Indirim { get; set; }

    public DateTime GecerlilikTarihi { get; set; }

    public bool Durum { get; set; }

    public int MusteriId { get; set; }

    public Musteri? Musteri { get; set; }
}
