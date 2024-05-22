using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Mutfak.Controllers
{
	[Area("Mutfak")]
	public class MutfakController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
