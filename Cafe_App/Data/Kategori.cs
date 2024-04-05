using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Kategori
{
    public int Id { get; set; }

    public string Ad { get; set; }

	public bool Gorunurluk { get; set; }
}
