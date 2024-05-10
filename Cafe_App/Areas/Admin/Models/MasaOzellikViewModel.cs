using Cafe_App.Models;

namespace Cafe_App.Areas.Admin.Models
{
	public class MasaOzellikViewModel
	{
		public Ozellik Ozellik { get; set; }

		public MasaOzellik MasaOzellik { get; set; }

		public List<Ozellik> Ozellikler { get; set; }

		public List<MasaOzellik> MasaOzellikler { get; set; }
	}
}
