using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MenuUrun
{
    public int Id { get; set; }

    public int UrunId { get; set; }

    public int MenuId { get; set; }

    public int Miktar { get; set; }

    public bool Gorunurluk { get; set; }

	public Menu Menu { get; set; } = null!;

    public Urun Urun { get; set; } = null!;
}
