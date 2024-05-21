using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index(string masa)
        {
            if (masa != null)
            {
                var masaObj = _context.Masalar.FirstOrDefault(x => x.Link == masa);
                if (masaObj != null)
                {
                    ViewBag.Masa = masaObj;
                    HttpContext.Session.SetString("MasaKodu", masaObj.Kod); // Masa kodunu session'a ekliyoruz
                }
            }

            return View();
        }


    }
}
