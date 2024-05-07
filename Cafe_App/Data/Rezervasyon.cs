using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Rezervasyon
{
    public int Id { get; set; }

    public int KayitsizMusteriId { get; set; }

	[Required(ErrorMessage = "Rezervasyon başlangıç saati olmamalıdır.")]
    public TimeOnly BaslangicSaat { get; set; }

	[Required(ErrorMessage = "Rezervasyon bitiş saati boş olmamalıdır.")]
	public TimeOnly BitisSaat { get; set; }

	[Required(ErrorMessage = "Personel soyadı boş olmamalıdır.")]
	public DateOnly Tarih { get; set; }

	[Required(ErrorMessage = "Rezervasyon tarihi boş olmamalıdır.")]
	public int KisiSayisi { get; set; }

	public string? Talep { get; set; }

    public int Onay { get; set; }

    public DateTime TalepTarihi { get; set; }

	public bool Gorunurluk { get; set; }
}
