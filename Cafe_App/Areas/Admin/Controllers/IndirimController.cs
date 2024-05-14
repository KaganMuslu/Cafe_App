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


		[AcceptVerbs("GET", "POST")]
		public IActionResult IndirimSecilenKontrol(IndirimViewModel model)
		{
			var messages = new List<string>();

			var menuler = model.SecilenMenuler.Where(val => val != "x").ToArray();
			var urunler = model.SecilenUrunler.Where(val => val != "x").ToArray();

			if (menuler.Length == 0 && urunler.Length == 0) 
			{ 
				messages.Add("İndirim için menü veya ürün seçilmelidir.");
			}
			else if (menuler.Length >= 1 && urunler.Length >= 1)
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
				messages.Add("İndirim miktar veya yüzde seçilmelidir.");
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

	}
}
