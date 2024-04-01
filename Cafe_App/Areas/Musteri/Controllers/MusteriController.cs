using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class MusteriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
