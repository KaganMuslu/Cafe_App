using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Rezervasyon
{
    public int Id { get; set; }

    public TimeOnly BaslangicSaat { get; set; }

    public TimeOnly BitisSaat { get; set; }

    public DateOnly Tarih { get; set; }

    public int KisiSayisi { get; set; }

    public string? Talep { get; set; }

    public int Onay { get; set; }

    public DateTime TalepTarihi { get; set; }

	public bool Gorunurluk { get; set; }
}
