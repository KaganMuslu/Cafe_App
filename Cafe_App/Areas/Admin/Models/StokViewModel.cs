using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class StokViewModel
	{
        public StokGirdi StokGirdi { get; set; }

        public Tedarikci Tedarikci { get; set; }

        public Malzeme Malzeme { get; set; }

        public List<StokGirdi> StokGirdiler { get; set; }

        public List<Stok> Stoklar { get; set; }

        public List<Malzeme> Malzemeler { get; set; }

        public List<Tedarikci> Tedarikciler { get; set; }
	}
}
