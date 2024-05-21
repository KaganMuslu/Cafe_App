using Cafe_App.Data;
using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Siparis
{
    public int Id { get; set; }

    public DateTime Tarih { get; set; }

    public string Kullanıcı { get; set; }

    public int DurumId { get; set; }

    public int? MasaId { get; set; }

    public int? AdresId { get; set; }

	public float Tutar { get; set; }

	public float OdenenTutar { get; set; }

	public bool OdemeDurum { get; set; }

    public string? Not { get; set; }

	public bool Gorunurluk { get; set; }

    public SiparisDurum SiparisDurum { get; set; }

    public Masa Masa { get; set; }

    public ICollection<TeslimatSiparis> Teslimatsiparislers { get; set; } = new List<TeslimatSiparis>();

    public ICollection<Odeme> Odemelers { get; set; } = new List<Odeme>();

    public ICollection<SiparisMenu> SiparisMenuler { get; set; } = new List<SiparisMenu>();

    public ICollection<SiparisUrun> SiparisUrunler { get; set; } = new List<SiparisUrun>();

}
