﻿using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class UrunMalzeme
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int UrunId { get; set; }

    public int MalzemeId { get; set; }

	public bool Gorunurluk { get; set; }

	public Malzeme Malzeme { get; set; } = null!;

    public Urun Urun { get; set; } = null!;
}
