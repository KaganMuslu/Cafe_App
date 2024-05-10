using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class RezervasyonViewModel
	{
		public Rezervasyon Rezervasyon { get; set; }
		public MasaRezervasyon MasaRezervasyon { get; set; }
		public Masa Masa { get; set; }
		public Kategori Kategori { get; set; }
		public List<Rezervasyon> Rezervasyonlar { get; set; }
		public List<MasaRezervasyon> MasaRezervasyonlar { get; set; }
		public List<Masa> Masalar { get; set; }
		public List<Kategori> Kategoriler { get; set; }
		
	}
}
