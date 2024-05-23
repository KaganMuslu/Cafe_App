using Cafe_App.Areas.Admin.Data;
using Cafe_App.Areas.Admin.Models;
using Cafe_App.Data;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
			var viewModel = new MenuViewModel
			{
				Menu = new Menu(),
				Menuler = _context.Menuler.Include(x => x.Kategori).ToList(),
				MenuUrunler = _context.MenuUrunler.Include(x => x.Urun).Include(x => x.Menu).Where(x => x.Gorunurluk == true).ToList(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Menu").ToList(),
				Urunler = _context.Urunler.Where(x => x.Gorunurluk == true).ToList(),
			};

			List<MenuIndirim> MenuIndirimler = [];
			foreach (var menuIndirim in _context.Menuler.Where(x => x.Gorunurluk == true).ToList())
			{
				var indirim = _context.MenuIndirimler.OrderByDescending(x => x.Id).FirstOrDefault(x => x.MenuId == menuIndirim.Id);
				if (indirim != null)
				{
					MenuIndirimler.Add(indirim);
				}
			}

			viewModel.MenuIndirimler = MenuIndirimler;

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(MenuViewModel model, IFormFile? file)
		{
			if (model.Menu != null)
			{
				if (model.Menu.KategoriId != 0)
				{
					if (file != null)
					{
						var uzanti = new[] { ".jpg", ".jpeg", ".png", ".webp" };
						var resimuzanti = Path.GetExtension(file.FileName);
						if (!uzanti.Contains(resimuzanti))
						{
							// Desteklenmeyen format hatası 
							ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
							return View(model);
						}

						var fotografRandom = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
						var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fotografRandom);
						using (var stream = new FileStream(resimyolu, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						model.Menu.Fotograf = fotografRandom;
					}
					else
					{
						var eskiFoto = _context.Menuler.Where(x => x.Id == model.Menu.Id).Select(x => x.Fotograf).FirstOrDefault();
						if (eskiFoto != null)
						{
							model.Menu.Fotograf = eskiFoto;
						}
						else
						{
							// Foto Girilmedi Hatası
						}
					}

					if (model.Menu.Id == 0)
					{
						_context.Add(model.Menu);
					}
					else
					{
						_context.Update(model.Menu);
					}

				}
				else
				{
					// Kategori Girilmedi Hatası
				}
			}
			else if (model.Kategori != null)
			{
				_context.Add(model.Kategori);
			}

			_context.SaveChanges();
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
		public IActionResult UrunSec(int menuId, string secilenUrunler)
		{
			var menu = JsonConvert.DeserializeObject<List<Uruns>>(secilenUrunler);

			var sorgu = _context.MenuUrunler.Where(x => x.MenuId == menuId).ToList();
			foreach (var sorguSil in sorgu)
			{
				_context.Remove(sorguSil);
			}

			foreach (var item in menu)
			{
				var urunSec = _context.Urunler.FirstOrDefault(x => x.Id == item.UrunId);

				if (urunSec != null)
				{
					_context.MenuUrunler.Add(new MenuUrun
					{
						MenuId = menuId,
						UrunId = urunSec.Id,
						Miktar = int.Parse(item.Miktar),
						Gorunurluk = true
					});
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

		[AcceptVerbs("GET", "POST")]
		public IActionResult MenuKontrol(MenuViewModel model)
		{
			var messages = new List<string>();

			var menuAd = _context.Menuler.FirstOrDefault(x => x.Ad == model.Menu.Ad && x.Gorunurluk == true && x.Id != model.Menu.Id);

			if (menuAd != null)
			{
				messages.Add("Bu ada sahip bir menü bulunmaktadır.");
			}

			// Toplu olarak döndür
			if (messages.Any())
			{
				return Json(messages);
			}

			return Json(true);
		}

	}

}
