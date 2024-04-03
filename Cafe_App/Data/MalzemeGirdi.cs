using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class MalzemeGirdi
{
    public int Id { get; set; }

    public int StokGirdiId { get; set; }

    public int MalzemeId { get; set; }

    public StokGirdi? StokGirdi { get; set; }

    public Malzeme? Malzeme { get; set; }
}
