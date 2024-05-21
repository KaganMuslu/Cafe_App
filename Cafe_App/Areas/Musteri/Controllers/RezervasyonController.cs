using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class RezervasyonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RezervasyonTalep()
        {
            return View();
        }
    }
}
