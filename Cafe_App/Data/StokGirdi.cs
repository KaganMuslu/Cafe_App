using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class StokGirdi
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int? SonStokMiktari { get; set; }

    public float AlısFiyati { get; set; }

	[Required(ErrorMessage = "Malzeme boş olamaz.")]
    public int MalzemeId { get; set; }

	public DateTime Tarih { get; set; }

	[Required(ErrorMessage = "Tedarikçi firma boş olamaz.")]
    public int TedarikciId { get; set; }

	public bool Gorunurluk { get; set; }

    public Malzeme? Malzeme { get; set; }

    public Tedarikci? Tedarikci { get; set; }
}
