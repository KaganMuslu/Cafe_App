using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class MusteriController : Controller
	{
		private readonly IdentityDataContext _context;
		public MusteriController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
            ViewBag.Musteriler = _context.Musteriler.ToList();
			return View();
		}

		public IActionResult MusteriEkle()
		{
			return View();
		}
	}
}
