using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UrunController : Controller
	{
		private readonly IdentityDataContext _context;
		public UrunController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
            ViewBag.Urunler = _context.Urunler.ToList();
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Index(Urun model)
		{
			if (ModelState.IsValid && model.KategoriId != 0)
			{
				var urun = _context.Urunler.FirstOrDefault(x => x.Ad == model.Ad);
				if (urun == null)
				{
					_context.Urunler.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

            ViewBag.Urunler = _context.Urunler.ToList();
			return RedirectToAction("Index");
		}
	}
}
