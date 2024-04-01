using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MalzemeGirdi
{
    public int Id { get; set; }

    public int GirdiId { get; set; }

    public int MalzemeId { get; set; }

    public StokGirdi? Girdi { get; set; }

    public Malzeme? Malzeme { get; set; }
}
