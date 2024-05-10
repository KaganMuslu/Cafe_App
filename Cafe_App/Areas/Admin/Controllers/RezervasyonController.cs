using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RezervasyonController : Controller
    {
		private readonly IdentityDataContext _context;
		public RezervasyonController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			// Modelleri ve kategorileri ayarlayın
			var viewModel = new RezervasyonViewModel
			{
				Rezervasyon = new Rezervasyon(),
				Masa = new Masa(),
				Kategori = new Kategori(),
				MasaRezervasyon = new MasaRezervasyon(),
				Rezervasyonlar = _context.Rezervasyonlar.ToList(),
				Masalar = _context.Masalar.ToList(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Masa").ToList(),

				MasaRezervasyonlar = _context.MasaRezervasyonlar
					.Include(x => x.Masa).Include(x => x.Rezervasyon).ToList()
			};
			
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index(RezervasyonViewModel model)
		{
			if (model.Rezervasyon != null)
			{
				_context.Add(model.Rezervasyon);
				_context.SaveChanges();
				if (model.MasaRezervasyon != null)
				{
					model.MasaRezervasyon.RezervasyonId = model.Rezervasyon.Id;
					_context.Add(model.MasaRezervasyon);
				}
			}
			else if(model.Kategori != null)
			{
				_context.Add(model.Kategori);
			}
			else if(model.Masa != null)
			{
				_context.Add(model.Masa);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult RezervasyonSil(int Id)
		{
			var rezervasyon = _context.Rezervasyonlar.FirstOrDefault(x => x.Id == Id);
			if (rezervasyon != null)
			{
				if (rezervasyon.Gorunurluk == true)
				{

					rezervasyon.Gorunurluk = false;
				}
				else
				{
					rezervasyon.Gorunurluk = true;
				}

				_context.Update(rezervasyon);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}


		[AcceptVerbs("GET", "POST")]
		public IActionResult SaatKontrol(RezervasyonViewModel model)
		{
			if (model.Rezervasyon.BitisSaat <= model.Rezervasyon.BaslangicSaat)
			{
				return Json("Rezervasyon bitiş saati başlangıç saatinden geç olmalıdır.");
			}

			return Json(true);
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult TarihKontrol(RezervasyonViewModel model)
		{
			var messages = new List<string>();

			// Veritabanındaki rezervasyonları kontrol etmek için sorgu
			bool rezervasyonVarMi = _context.Rezervasyonlar
				.Any(r => r.Tarih == model.Rezervasyon.Tarih && (
					(model.Rezervasyon.BaslangicSaat >= r.BaslangicSaat && model.Rezervasyon.BaslangicSaat < r.BitisSaat) ||
					(model.Rezervasyon.BitisSaat > r.BaslangicSaat && model.Rezervasyon.BitisSaat <= r.BitisSaat) ||
					(model.Rezervasyon.BaslangicSaat <= r.BaslangicSaat && model.Rezervasyon.BitisSaat >= r.BitisSaat)
				));

			if (rezervasyonVarMi)
			{
				messages.Add("Bu tarihte bu saatler arasında rezervasyon oluşturulmuştur.");
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
