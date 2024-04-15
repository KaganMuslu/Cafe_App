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
			ViewBag.urunGruplari = _context.Urunler
				.Join(_context.Kategoriler,
					urun => urun.KategoriId,
					kategori => kategori.Id,
					(urun, kategori) => new { Urun = urun, KategoriAdi = kategori.Ad })
				.GroupBy(u => u.KategoriAdi)
				.Select(g => new
				{
					KategoriAdi = g.Key,
					UrunListesi = g.Select(u => u.Urun).ToList()
				})
				.ToList();

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
