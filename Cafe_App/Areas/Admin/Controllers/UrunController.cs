using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;

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
            ViewBag.Urunler = _context.Urunler.Include(x => x.Kategori).ToList();
            ViewBag.UrunMalzeme = _context.UrunMalzemeler.Include(x => x.Urun).Include(x => x.Malzeme).Where(x => x.Gorunurluk == true).ToList();
			ViewBag.Kategoriler = _context.Kategoriler.ToList();
			ViewBag.Malzemeler = _context.Malzemeler.Where(x => x.Gorunurluk == true).ToList();

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(Urun model, IFormFile? file)
		{
			ViewBag.Urunler = _context.Urunler.ToList();

			if (ModelState.IsValid && model.KategoriId != 0)
			{
				if (file != null)
				{
					var uzanti = new[] { ".jpg", ".jpeg", ".png" };
					var resimuzanti = Path.GetExtension(file.FileName);
					if (!uzanti.Contains(resimuzanti))
					{
						ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
						return View(model);
					}
				}
				else
				{
					ModelState.AddModelError("OgrenciFotograf", "Fotoğraf boş olamaz");
					return View(model);
				}

				var fotografRandom = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
				var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fotografRandom);
				using (var stream = new FileStream(resimyolu, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				model.Fotograf = fotografRandom;

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
		public async Task<IActionResult> UrunGuncelle(Urun model, IFormFile? file)
		{
            ViewBag.Kategoriler = _context.Kategoriler.ToList();

			if (file != null)
			{
				var uzanti = new[] { ".jpg", ".jpeg", ".png" };
				var resimuzanti = Path.GetExtension(file.FileName);
				if (!uzanti.Contains(resimuzanti))
				{
					ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
					return View(model);
				}

				var fotografRandom = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
				var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fotografRandom);
				using (var stream = new FileStream(resimyolu, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				model.Fotograf = fotografRandom;
			}
			else
			{
				model.Fotograf = _context.Urunler.Where(x => x.Id == model.Id).Select(x => x.Fotograf).FirstOrDefault();
			}

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

		[HttpPost]
		public IActionResult MalzemeSec(int urunId, List<string> secilenMalzemeler)
		{
			if (urunId <= 0 || secilenMalzemeler == null || secilenMalzemeler.Count == 0)
			{
				// Hata durumu işlemleri
				return RedirectToAction("Index");
			}

			var malzemeler = secilenMalzemeler[0];

			JArray malzemeArray = JArray.Parse(malzemeler);

			foreach (var malzeme in malzemeArray)
			{
				// İlk öğeyi alıyoruz
				JObject ilkMalzeme = (JObject)malzeme;

				// Malzeme ve miktarını alıyoruz
				string malzemeAdi = ilkMalzeme.Properties().First().Name;
				int miktar = (int)ilkMalzeme[malzemeAdi];

				var malzemeSec = _context.Malzemeler.FirstOrDefault(x => x.Ad == malzemeAdi);

				var sorgu = _context.UrunMalzemeler.FirstOrDefault(x => x.UrunId == urunId && x.MalzemeId == malzemeSec.Id);

				if(sorgu == null)
				{
					var urunMalzeme = new UrunMalzeme
					{
						UrunId = urunId,
						MalzemeId = malzemeSec.Id,
						Miktar = miktar,
						Gorunurluk = true // Varsayılan olarak görünürlük true olarak ayarlanabilir
					};

					_context.UrunMalzemeler.Add(urunMalzeme);
				}
				else if (sorgu.Gorunurluk == false)
				{
					sorgu.Miktar = miktar;
					sorgu.Gorunurluk = true;
				}
				else if (sorgu.Miktar != miktar)
				{
					sorgu.Miktar = miktar;
				}
				else
				{
					// Hata gönder bu malzeme zaten ekli!: Malzeme adı
					break;
				}
				
			}
			
			_context.SaveChanges();

			return RedirectToAction("Index");
		}


		public IActionResult MalzemeSil(int Id, int malzemeId)
		{
			var urunMalzeme = _context.UrunMalzemeler.Where(x => x.UrunId == Id && x.MalzemeId == malzemeId).FirstOrDefault();
			if (urunMalzeme != null)
			{
				urunMalzeme.Gorunurluk = false;

				_context.Update(urunMalzeme);
				_context.SaveChanges();
			}
			// Urune ait malzeme bulunamadı

			return RedirectToAction("Index");
		}
	}

}
