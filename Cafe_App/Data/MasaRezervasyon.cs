using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MasaRezervasyon
{
    public int Id { get; set; }

    public int RezervasyonId { get; set; }

    public int MasaId { get; set; }

	public bool Gorunurluk { get; set; }

	public Masa Masa { get; set; } = null!;

    public Rezervasyon Rezervasyon { get; set; } = null!;
}
