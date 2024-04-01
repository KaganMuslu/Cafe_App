﻿using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class SiparisUrun
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int SiparisId { get; set; }

    public int UrunId { get; set; }

    public Siparis Siparis { get; set; } = null!;

    public Urun Urun { get; set; } = null!;
}
