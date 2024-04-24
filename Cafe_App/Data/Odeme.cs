using Cafe_App.Models;

namespace Cafe_App.Data
{
    public class Odeme
    {
        public int Id { get; set; }

        public int Tur { get; set; }

        public float Tutar { get; set; }

        public int SiparisId { get; set; }

        public Siparis? Siparis { get; set; }

    }
}
