using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
				if (model.Id == 0)
				{
					var kategori = _context.Kategoriler.FirstOrDefault(x => x.Ad == model.Ad);
					if (kategori == null)
					{
						_context.Kategoriler.Add(model);
					}
					else
					{
						// Hata
					}
				}
				else
				{
					_context.Update(model);
				}
					
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult KategoriSil(int Id)
		{
			var kategori = _context.Kategoriler.FirstOrDefault(x => x.Id == Id);
			if (kategori != null)
			{
				if (kategori.Gorunurluk == true)
				{
					kategori.Gorunurluk = false;
				}
				else
				{
					kategori.Gorunurluk = true;
				}
				_context.Update(kategori);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		public IActionResult KategoriGuncelle(int Id) // MODAL YAPILABİLİR
		{
			var kategori = _context.Kategoriler.FirstOrDefault(x => x.Id == Id);

			return View(kategori);
		}

		[HttpPost]
		public IActionResult KategoriGuncelle(Kategori model)
		{
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
