using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Musteri.Controllers
{
    public class HakkımızdaController : Controller
    {
        [Area("Musteri")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
