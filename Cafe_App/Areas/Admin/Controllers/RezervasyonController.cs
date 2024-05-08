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
			ViewBag.Rezervasyonlar = _context.Rezervasyonlar.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Index(Rezervasyon model)
		{
			if (ModelState.IsValid)
			{
				_context.Rezervasyonlar.Add(model);
				_context.SaveChanges();
			}

			ViewBag.Rezervasyonlar = _context.Rezervasyonlar.ToList();
			return View(model);
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult SaatKontrol(Rezervasyon model)
		{
			if (model.BitisSaat <= model.BaslangicSaat)
			{
				return Json("Rezervasyon bitiş saati başlangıç saatinden geç olmalıdır.");
			}

			return Json(true);
		}
	}
}
