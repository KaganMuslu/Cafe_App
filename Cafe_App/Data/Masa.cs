using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Masa
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Masa kodu boş olmamalıdır.")]
	[Remote(action: "MasaKontrol", controller: "Masa", HttpMethod = "POST", AdditionalFields = nameof(Kod) + "," + nameof(Id))]
    public string Kod { get; set; }

    public string Link { get; set; }

	public int Durum { get; set; }

	[Required(ErrorMessage = "Masa kapasitesi boş olmamalıdır.")]
    public int Kapasite { get; set; }

    public int? PersonelId { get; set; }

	[Required(ErrorMessage = "Masa kategorisi boş olmamalıdır.")]
    public int KategoriId { get; set; }

	public string? QR { get; set; }

	public bool Gorunurluk { get; set; }

	public ICollection<MasaSiparis> MasaSipariss { get; set; } = new List<MasaSiparis>();

    public Personel? Personel { get; set; }

    public Kategori? Kategori { get; set; }

	public ICollection<MasaOzellik> MasaOzellikler { get; set; } = new List<MasaOzellik>();

}
