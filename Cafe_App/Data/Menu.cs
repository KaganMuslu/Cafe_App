using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public string Aciklama { get; set; }

    public int Fiyat { get; set; }

    public string Detay { get; set; }

    public string Fotograf { get; set; }

    public bool Akitf { get; set; }

    public int IndirimliFiyat { get; set; }
}
