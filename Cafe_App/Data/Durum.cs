using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Durum
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public TimeOnly Zaman { get; set; }

    public string Yer { get; set; }

    public int SiparisId { get; set; }

    public Siparis? Siparis { get; set; }
}
