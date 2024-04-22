using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Rezervasyon
{
    public int Id { get; set; }

    public DateOnly BaslangıcSaat { get; set; }

    public DateOnly BıtısSaat { get; set; }

    public DateOnly Tarih { get; set; }

    public int KisiSayisi { get; set; }

    public string Talep { get; set; }

    public bool Onay { get; set; }

    public DateOnly TalepTarihi { get; set; }

	public bool Gorunurluk { get; set; }
}
