using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RezervasyonController : Controller
    {
		private readonly IdentityDataContext _context;
		public RezervasyonController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			ViewBag.Rezervasyonlar = _context.Tedarikciler.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Index(Tedarikci model)
		{
			if (ModelState.IsValid)
			{
				var tedarikci = _context.Tedarikciler.FirstOrDefault(x => x.Eposta == model.Eposta);
				if (tedarikci == null)
				{
					_context.Tedarikciler.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

			ViewBag.Tedarikciler = _context.Tedarikciler.ToList();
			return RedirectToAction("Index");
		}
	}
}
