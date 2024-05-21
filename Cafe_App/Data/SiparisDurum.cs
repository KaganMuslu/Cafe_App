using Cafe_App.Models;

namespace Cafe_App.Data
{
	public class SiparisDurum
	{
        public int Id { get; set; }

        public int SiparisId { get; set; }

        public int DurumId { get; set; }

		public DateOnly Tarih { get; set; }

        public Siparis Siparis { get; set; }

        public Durum Durum { get; set; }
    }
}
