using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MenuController : Controller
	{
		private readonly IdentityDataContext _context;
		public MenuController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			ViewBag.Menuler = _context.Menuler.Include(x => x.Kategori).ToList();
			ViewBag.MenuUrun = _context.MenuUrunler.Include(x => x.Urun).Include(x => x.Menu).Where(x => x.Gorunurluk == true).ToList();
			ViewBag.Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Menu").ToList();
			ViewBag.Urunler = _context.Urunler.Where(x => x.Gorunurluk == true).ToList();

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(Menu model, IFormFile? file)
		{
			ViewBag.Menuler = _context.Menuler.ToList();

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

		public IActionResult MenuGuncelle(int Id)
		{
			ViewBag.Kategoriler = _context.Kategoriler.ToList();
			var menu = _context.Menuler.FirstOrDefault(x => x.Id == Id);
			return View(menu);
		}

		[HttpPost]
		public async Task<IActionResult> MenuGuncelle(Menu model, IFormFile? file)
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

		public IActionResult MenuSil(int Id)
		{
			var menu = _context.Menuler.FirstOrDefault(x => x.Id == Id);
			if (menu != null)
			{
				if (menu.Gorunurluk == true)
				{
					menu.Gorunurluk = false;
				}
				else
				{
					menu.Gorunurluk = true;
				}
				_context.Update(menu);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult UrunSec(int menuId, List<string> secilenUrunler)
		{
			if (menuId <= 0 || secilenUrunler == null || secilenUrunler.Count == 0)
			{
				// Hata durumu işlemleri
				return RedirectToAction("Index");
			}

			var malzemeler = secilenUrunler[0];

			JArray malzemeArray = JArray.Parse(malzemeler);

			foreach (var malzeme in malzemeArray)
			{
				// İlk öğeyi alıyoruz
				JObject ilkUrun = (JObject)malzeme;

				// Malzeme ve miktarını alıyoruz
				string urunAdi = ilkUrun.Properties().First().Name;
				int miktar = (int)ilkUrun[urunAdi];

				var urunSec = _context.Urunler.FirstOrDefault(x => x.Ad == urunAdi);

				var sorgu = _context.MenuUrunler.FirstOrDefault(x => x.MenuId == menuId && x.UrunId == urunSec.Id);

				if (sorgu == null)
				{
					var menuUrun = new MenuUrun
					{
						MenuId = menuId,
						UrunId = urunSec.Id,
						Miktar = miktar,
						Gorunurluk = true // Varsayılan olarak görünürlük true olarak ayarlanabilir
					};

					_context.MenuUrunler.Add(menuUrun);
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

		public IActionResult UrunSil(int Id, int urunId)
		{
			var menuUrun = _context.MenuUrunler.Where(x => x.MenuId == Id && x.UrunId == urunId).FirstOrDefault();
			if (menuUrun != null)
			{
				menuUrun.Gorunurluk = false;

				_context.Update(menuUrun);
				_context.SaveChanges();
			}
			// Urune ait malzeme bulunamadı

			return RedirectToAction("Index");
		}
	}

}
