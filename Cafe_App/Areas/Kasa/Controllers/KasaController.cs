using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Kasa.Controllers
{
    [Area("Kasa")]
    public class KasaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
