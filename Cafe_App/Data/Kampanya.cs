using Cafe_App.Models;

namespace Cafe_App.Data
{
	public class Kampanya
	{
		public int Id { get; set; }

		public string Ad { get; set; }

		public float Indirim { get; set; }

		public int SartTutar { get; set; }

		public int SartSiparisSayisi { get; set; }

		public DateTime BaslangıcTarihi { get; set; }

		public DateTime BitisTarihi { get; set; }

		public bool Gorunurluk { get; set; }

		public ICollection<KampanyaMusteri> KampanyaMusteris { get; set; } = new List<KampanyaMusteri>();
	}
}
