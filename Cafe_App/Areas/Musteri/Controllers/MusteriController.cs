using Cafe_App.Areas.Admin.Models;
using Cafe_App.Areas.Musteri.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class MusteriController : Controller
    {
        private readonly IdentityDataContext _context;
        public MusteriController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index(bool SiparisAlındı)
        {
			if (SiparisAlındı == true)
			{
				ViewBag.SiparisAlındı = true;
			}

			var simdikiTarih = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.MenuIndirimler = _context.MenuIndirimler
                .Where(x => x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih)
                .ToList();

            var menuler = _context.Menuler.Include(x => x.Kategori).Where(x => x.Gorunurluk == true).ToList();
            var randomMenuler = menuler.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

            var urunler = _context.Urunler.Include(x => x.Kategori).Where(x => x.Gorunurluk == true).ToList();
            var randomUrunler = urunler.OrderBy(x => Guid.NewGuid()).Take(8).ToList();

            var viewModel = new MusteriSayfaViewModel
            {
                Urunler = randomUrunler,
                Menuler = randomMenuler,
            };

            ViewBag.UrunIndirimler = _context.UrunIndirimler
                .Where(x => x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih)
                .ToList();

            return View(viewModel);
        }


    }
}
