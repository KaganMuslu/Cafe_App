using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Masa
{
    public int Id { get; set; }

    public string Kod { get; set; }

    public int Durum { get; set; }

    public int Kapasite { get; set; }

    public float Tutar { get; set; }

    public float OdenenTutar { get; set; }

    public int PersonelId { get; set; }

    public int KategoriId { get; set; }

	public bool Gorunurluk { get; set; }

	public ICollection<MasaOzellik> MasaOzelliks { get; set; } = new List<MasaOzellik>();

    public Personel? Personel { get; set; }
}
