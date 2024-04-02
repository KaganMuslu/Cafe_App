using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MasaOzellik
{
    public int Id { get; set; }

    public int OzellikId { get; set; }

    public int MasaId { get; set; }

	public bool Gorunurluk { get; set; }

	public Masa Masa { get; set; } = null!;

    public Ozellik Ozellik { get; set; } = null!;
}
