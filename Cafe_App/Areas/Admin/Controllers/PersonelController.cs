using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class PersonelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult PersonelEkle()
		{
			return View();
		}
	}
}
