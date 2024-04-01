using System;
using System.Collections.Generic;

namespace Cafe_App.Models;

public partial class Tedarikci
{
    public int Id { get; set; }

    public string Ad { get; set; }

    public string Soyad { get; set; }

    public string Telefon { get; set; }

    public int AdresId { get; set; }

    public Adres? Adres { get; set; }

    public ICollection<StokGirdi> Stokgirdilers { get; set; } = new List<StokGirdi>();

    public ICollection<Stok> Stoklars { get; set; } = new List<Stok>();
}
