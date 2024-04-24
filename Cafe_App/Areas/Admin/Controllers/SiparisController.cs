using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
	public class SiparisController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
