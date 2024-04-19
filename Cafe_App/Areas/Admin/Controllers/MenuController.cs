using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
	public class MenuController : Controller
	{
		private readonly IdentityDataContext _context;
		public MenuController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
