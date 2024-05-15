using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cafe_App.Areas.Admin.Models
{
	public class IndirimViewModel
	{
		public Urun Urun { get; set; }

		public Menu Menu { get; set; }

		public Kategori Kategori { get; set; }

		public List<Urun> Urunler { get; set; }

		public List<Menu> Menuler { get; set; }

		public List<Kategori> Kategoriler { get; set; }

        public List<MenuIndirim> MenuIndirimler { get; set; }

        public List<UrunIndirim> UrunIndirimler { get; set; }

		public string[]? SecilenUrunler { get; set; }

		[Remote(action: "IndirimSecilenKontrol", controller: "Indirim", HttpMethod = "POST", AdditionalFields = nameof(SecilenUrunler) + "," + nameof(SecilenMenuler) )]
		public string[]? SecilenMenuler { get; set; }

		public int IndirimMiktari { get; set; }

		[Remote(action: "IndirimMiktarKontrol", controller: "Indirim", HttpMethod = "POST", AdditionalFields = nameof(IndirimMiktari) + "," + nameof(IndririmYuzdesi) )]
		public int IndririmYuzdesi { get; set; }

		[Required(ErrorMessage = "İndirim başlangıç tarihi boş olmamalıdır.")]
		public DateOnly BaslangıcTarihi { get; set; }

		[Required(ErrorMessage = "İndirim bitiş tarihi boş olmamalıdır.")]
		[Remote(action: "IndirimTarihKontrol", controller: "Indirim", HttpMethod = "POST", AdditionalFields = nameof(BaslangıcTarihi) + "," + nameof(BitisTarihi) )]
		public DateOnly BitisTarihi { get; set; }

		public DateOnly OlusturmaTarihi { get; set; }
	}
}
