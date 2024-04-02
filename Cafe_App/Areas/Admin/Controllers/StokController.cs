using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class StokController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
