using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class SiparisMenu
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int MenuId { get; set; }

    public int SiparisId { get; set; }

    public Menu Menu { get; set; } = null!;

    public Siparis Siparis { get; set; } = null!;
}
