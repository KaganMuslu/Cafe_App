using Cafe_App.Models;

namespace Cafe_App.Areas.Musteri.Models
{
    public class UrunViewModel
    {
        public List<Urun> Urunler { get; set; }
        public List<UrunIndirim> UrunIndirimler { get; set; }
    }
}
