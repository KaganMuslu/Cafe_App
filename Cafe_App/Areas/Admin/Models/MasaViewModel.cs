using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class MasaViewModel
	{
		public Masa Masa { get; set; }
		public Kategori Kategori { get; set; }
		public List<Kategori> Kategoriler { get; set; }
		public List<Masa> Masalar { get; set; }
	}
}
