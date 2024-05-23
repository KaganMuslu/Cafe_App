using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class UrunViewModel
	{
        public Urun Urun { get; set; }

        public Kategori Kategori { get; set; }

		public List<Urun> Urunler { get; set; }

		public List<UrunIndirim> UrunIndirimler { get; set; }

		public List<Malzeme> Malzemeler { get; set; }

        public List<UrunMalzeme> UrunMalzemeler { get; set; }

        public List<Kategori> Kategoriler { get; set; }

		public string SecilenMalzemeler { get; set; }
	}
}
