using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class KategoriController : Controller
	{
		private readonly IdentityDataContext _context;
		public KategoriController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Index(Kategori model)
		{
			if (ModelState.IsValid)
			{
				var kategori = _context.Kategoriler.FirstOrDefault(x => x.Ad == model.Ad);
				if (kategori == null)
				{
					_context.Kategoriler.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

			ViewBag.Kategoriler = _context.Kategoriler.ToList();
			return RedirectToAction("Index");
		}
	}
}
