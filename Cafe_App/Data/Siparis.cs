using Cafe_App.Data;
using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Siparis
{
    public int Id { get; set; }

    public DateTime Tarih { get; set; }

    public int? AdresId { get; set; }

    public decimal Tutar { get; set; }

    public bool OdemeDurum { get; set; }

    public string Not { get; set; }

	public bool Gorunurluk { get; set; }

	public ICollection<Durum> Durumlars { get; set; } = new List<Durum>();

    public ICollection<TeslimatSiparis> Teslimatsiparislers { get; set; } = new List<TeslimatSiparis>();

    public ICollection<Odeme> Odemelers { get; set; } = new List<Odeme>();

    public ICollection<SiparisMenu> SiparisMenuler { get; set; } = new List<SiparisMenu>();

    public ICollection<SiparisUrun> SiparisUrunler { get; set; } = new List<SiparisUrun>();

}
