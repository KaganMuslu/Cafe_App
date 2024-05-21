using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Rezervasyon
{
    public int Id { get; set; }

    public int KayitsizMusteriId { get; set; }

    /*public int TelefonNumarası { get; set; }*/

    [Required(ErrorMessage = "Rezervasyon başlangıç saati olmamalıdır.")]
	[Remote(action: "SaatKontrol", controller: "Rezervasyon", HttpMethod = "POST", AdditionalFields = nameof(BaslangicSaat) + "," + nameof(BitisSaat))]
    public TimeOnly BaslangicSaat { get; set; }

	[Required(ErrorMessage = "Rezervasyon bitiş saati boş olmamalıdır.")]
	[Remote(action: "SaatKontrol", controller: "Rezervasyon", HttpMethod = "POST", AdditionalFields = nameof(BaslangicSaat) + "," + nameof(BitisSaat))]
	public TimeOnly BitisSaat { get; set; }

	[Required(ErrorMessage = "Personel soyadı boş olmamalıdır.")]
	[Remote(action: "TarihKontrol", controller: "Rezervasyon", HttpMethod = "POST", AdditionalFields = nameof(Tarih) + "," + nameof(BaslangicSaat) + "," + nameof(BitisSaat))]
	public DateOnly Tarih { get; set; }

	[Required(ErrorMessage = "Rezervasyon tarihi boş olmamalıdır.")]
	public int KisiSayisi { get; set; }

	public string? Talep { get; set; }

    public int Onay { get; set; }

    public DateTime TalepTarihi { get; set; }

	public bool Gorunurluk { get; set; }
}
