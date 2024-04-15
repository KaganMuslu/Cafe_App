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
            ViewBag.Urunler = _context.Urunler.ToList();

			if (ModelState.IsValid && model.KategoriId != 0)
			{

				if (model.Id == 0)
				{
					_context.Add(model);
				}
				else
				{
					_context.Update(model);
				}
				_context.SaveChanges();
				
			}

			return RedirectToAction("Index");
		}

		public IActionResult UrunGuncelle(int Id)
		{
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
			var urun = _context.Urunler.FirstOrDefault(x => x.Id == Id);
			return View(urun);
		}

		[HttpPost]
		public IActionResult UrunGuncelle(Urun model)
		{
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
			_context.Update(model);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult UrunSil(int Id)
		{
			var urun = _context.Urunler.FirstOrDefault(x => x.Id == Id);
			if (urun != null)
			{
				if (urun.Gorunurluk == true)
				{
					urun.Gorunurluk = false;
				}
				else
				{
					urun.Gorunurluk = true;
				}
				_context.Update(urun);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}
