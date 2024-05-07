using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Stok
{
    public int Id { get; set; }

    public int Miktar { get; set; }

	[Required(ErrorMessage = "Minimum stok boş olamaz.")]
    public int MinStok { get; set; }

	[Required(ErrorMessage = "Maximum stok boş olamaz.")]
    public int MaxStok { get; set; }

	public bool Gorunurluk { get; set; }

    public int MalzemeId { get; set; }

	public Malzeme? Malzeme { get; set; }
}
