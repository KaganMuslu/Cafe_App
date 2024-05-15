using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IndirimController : Controller
    {
		private readonly IdentityDataContext _context;
		public IndirimController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
			var viewModel = new IndirimViewModel
			{
				Urun = new Urun(),
				Menu = new Menu(),
				Kategori = new Kategori(),
				Urunler = _context.Urunler.Include(x => x.Kategori).ToList(),
				Menuler = _context.Menuler.Include(x => x.Kategori).ToList(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Urun" || x.Tur == "Menu").ToList(),
			};

			return View(viewModel);
        }

		[HttpPost]
		public IActionResult UrunIndirim(IndirimViewModel model)
		{
			if ((model.SecilenMenuler != null ||  model.SecilenUrunler != null) && (model.IndirimMiktari != null || model.IndririmYuzdesi != null))
			{
				var menuler = model.SecilenMenuler;
				var urunler = model.SecilenUrunler;

				/*if (model.SecilenMenuler != null)
				{
					menuler = model.SecilenMenuler[0].Split(',').ToArray();
				}

				if (model.SecilenUrunler != null)
				{
					urunler = model.SecilenUrunler[0].Split(',').ToArray();
				}*/

				if (urunler != null)
				{
					foreach (var urunIndirim in urunler)
					{
						var urun = _context.Urunler.FirstOrDefault(x => x.Id == Convert.ToInt32(urunIndirim));

						if (model.IndirimMiktari != 0)
						{
							UrunIndirim yeniIndirim = new UrunIndirim
							{
								UrunId = Convert.ToInt32(urunIndirim),
								IndirimMiktari = model.IndirimMiktari,
								IndirimYuzdesi = null,
								BaslangıcTarihi = model.BaslangıcTarihi,
								BitisTarihi = model.BitisTarihi,
								OlusturmaTarihi = DateTime.Now,
								Gorunurluk = true
							};

							_context.Add(yeniIndirim);
						}
						else
						{
							UrunIndirim yeniIndirim = new UrunIndirim
							{
								UrunId = Convert.ToInt32(urunIndirim),
								IndirimMiktari = null,
								IndirimYuzdesi = model.IndririmYuzdesi,
								BaslangıcTarihi = model.BaslangıcTarihi,
								BitisTarihi = model.BitisTarihi,
								OlusturmaTarihi = DateTime.Now,
								Gorunurluk = true
							};

							_context.Add(yeniIndirim);
						}
					}
				}
				else
				{
					foreach (var menuIndirim in menuler)
					{
						var menu = _context.Menuler.FirstOrDefault(x => x.Id == Convert.ToInt32(menuIndirim));

						if (model.IndirimMiktari != 0)
						{
							MenuIndirim yeniIndirim = new MenuIndirim
							{
								MenuId = Convert.ToInt32(menuIndirim),
								IndirimMiktari = model.IndirimMiktari,
								IndirimYuzdesi = null,
								BaslangıcTarihi = model.BaslangıcTarihi,
								BitisTarihi = model.BitisTarihi,
								OlusturmaTarihi = DateTime.Now,
								Gorunurluk = true
							};

							_context.Add(yeniIndirim);
						}
						else
						{
							MenuIndirim yeniIndirim = new MenuIndirim
							{
								MenuId = Convert.ToInt32(menuIndirim),
								IndirimMiktari = null,
								IndirimYuzdesi = model.IndririmYuzdesi,
								BaslangıcTarihi = model.BaslangıcTarihi,
								BitisTarihi = model.BitisTarihi,
								OlusturmaTarihi = DateTime.Now,
								Gorunurluk = true
							};

							_context.Add(yeniIndirim);
						}
					}
				}

				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}


		[AcceptVerbs("GET", "POST")]
		public IActionResult IndirimSecilenKontrol(IndirimViewModel model)
		{
			var messages = new List<string>();

			var menuler = model.SecilenMenuler;
			var urunler = model.SecilenUrunler;

			if (model.SecilenMenuler[0] != null)
			{
				menuler = model.SecilenMenuler[0].Split(',').ToArray();
			}

			if (model.SecilenUrunler[0] != null)
			{
				urunler = model.SecilenUrunler[0].Split(',').ToArray();
			}

			if (menuler[0] == null && urunler[0] == null) 
			{ 
				messages.Add("İndirim için menü veya ürün seçilmelidir.");
			}
			else if (menuler[0] != null && urunler[0] != null)
			{
				messages.Add("İndirim menü ve ürün aynı anda seçilmemelidir.");
			}

			// Toplu olarak döndür
			if (messages.Any())
			{
				return Json(messages);
			}

			return Json(true);
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult IndirimMiktarKontrol(IndirimViewModel model)
		{
			var messages = new List<string>();

			if (model.IndirimMiktari == 0 && model.IndririmYuzdesi == 0)
			{
				messages.Add("İndirim miktar veya yüzde girilmemelidir.");
			}
			else if (model.IndirimMiktari >= 1 && model.IndririmYuzdesi >= 1)
			{
				messages.Add("İndirim miktar ve yüzde aynı anda girilmemelidir.");
			}

			// Toplu olarak döndür
			if (messages.Any())
			{
				return Json(messages);
			}

			return Json(true);
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult IndirimTarihKontrol(IndirimViewModel model)
		{
			if (model.BaslangıcTarihi <= DateOnly.FromDateTime(DateTime.Now))
			{
				return Json("Başlangıç tarihi gelecek zaman olmalıdır.");
			}

			if (model.BitisTarihi <= model.BaslangıcTarihi)
			{
				return Json("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
			}

			return Json(true);
		}

	}
}
