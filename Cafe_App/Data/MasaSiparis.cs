using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MasaSiparis
{
    public int Id { get; set; }

    public int MasaId { get; set; }

    public int SiparisId { get; set; }

	public bool Gorunurluk { get; set; }

	public Masa Masa { get; set; } = null!;

    public Siparis Siparis { get; set; } = null!;
}
