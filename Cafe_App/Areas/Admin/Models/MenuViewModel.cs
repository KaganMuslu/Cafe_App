using Cafe_App.Models;
using System.Collections.Generic;

namespace Cafe_App.Areas.Admin.Models
{
	public class MenuViewModel
	{
		public Menu Menu { get; set; }
		public Kategori Kategori { get; set; }
		public List<MenuUrun> MenuUrunler { get; set; }
		public List<Menu> Menuler { get; set; }
		public List<Kategori> Kategoriler { get; set; }
		public List<Urun> Urunler { get; set; }
		public string SecilenUrunler { get; set; }
	}
}
