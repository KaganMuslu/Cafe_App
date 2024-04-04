using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class MusteriController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult MusteriEkle()
		{
			return View();
		}
	}
}
