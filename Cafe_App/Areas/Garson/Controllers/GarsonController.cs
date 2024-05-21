using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Garson.Controllers
{
    [Area("Garson")]
    public class GarsonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
