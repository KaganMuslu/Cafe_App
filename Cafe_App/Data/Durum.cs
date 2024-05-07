using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Durum
{
    public int Id { get; set; }

    public int Ad { get; set; }

    public DateTime Zaman { get; set; }

    public int Yer { get; set; }

    public int SiparisId { get; set; }

    public Siparis? Siparis { get; set; }
}
