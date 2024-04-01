﻿using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Stok
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int MinStok { get; set; }

    public int MaxStok { get; set; }

    public int TedarikciId { get; set; }

    public ICollection<Malzeme> Malzemelers { get; set; } = new List<Malzeme>();

    public Tedarikci? Tedarikci { get; set; }
}
