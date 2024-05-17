using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class MenuIndirim
{
    public int Id { get; set; }

	public int MenuId { get; set; }

    public int? IndirimMiktari { get; set; }

    public int? IndirimYuzdesi { get; set; }

    public float? YeniFiyat { get; set; }

    public DateOnly BaslangıcTarihi { get; set; }

    public DateOnly BitisTarihi { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public bool Gorunurluk { get; set; }

    public Menu? Menu { get; set; }

}