using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public bool Gorunurluk { get; set; }

	public ICollection<Personel> Personellers { get; set; } = new List<Personel>();
}