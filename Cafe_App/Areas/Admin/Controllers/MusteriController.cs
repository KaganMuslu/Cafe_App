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

		[HttpPost]
		public IActionResult Index(Cafe_App.Models.Musteri model)
		{
			if (ModelState.IsValid)
			{
				var musteri = _context.Musteriler.FirstOrDefault(x => x.Eposta == model.Eposta);
				if (musteri == null)
				{
					_context.Musteriler.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

			ViewBag.Musteriler = _context.Musteriler.ToList();
			return RedirectToAction("Index");
		}
	}
}
