using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class StokGirdi
{
    public int Id { get; set; }

    public int Miktar { get; set; }

    public int AlısFiyati { get; set; }

    public DateTime Tarih { get; set; }

    public int TedarikciId { get; set; }

    public Tedarikci? Tedarikci { get; set; }
}
