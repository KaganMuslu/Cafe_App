using Cafe_App.Areas.Admin.Models;
using Cafe_App.Areas.Garson.Models;
using Cafe_App.Data;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Garson.Controllers
{
	[Area("Garson")]
	public class OnaylanacakController : Controller
	{
		private readonly IdentityDataContext _context;
		public OnaylanacakController(IdentityDataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var viewModel = new SiparislerModelView
			{
				OnaysizSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => (x.DurumId == 1 || x.DurumId == 4) && x.Gorunurluk == true).ToList(),
				GecmisSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => (x.DurumId != 1 && x.DurumId != 4) && x.Gorunurluk == true).ToList(),
				SiparisUrunler = _context.SiparisUrunler.Include(x => x.Urun).ToList(),
				SiparisMenuler = _context.SiparisMenuler.Include(x => x.Menu).ToList()

			};

			List<SiparisDurum> siparisDurumlar = [];
			foreach (var siparis in _context.Siparisler.Where(x => x.Gorunurluk == true).ToList())
			{
				var siparisDurum = _context.SiparisDurumlar.Include(x => x.Durum).OrderByDescending(x  => x.Id).FirstOrDefault(x => x.SiparisId == siparis.Id);
				if (siparisDurum != null)
				{
					siparisDurumlar.Add(siparisDurum);
				}
			}

			viewModel.SiparisDurumlar = siparisDurumlar;


			return View(viewModel);
		}

		public IActionResult SiparisOnayla(int id, int teslim)
		{
			var siparis = _context.Siparisler.FirstOrDefault(x => x.Id == id);
			if (siparis != null)
			{
				if (teslim != 1)
				{
					siparis.DurumId = 2;
				}
				else
				{
					siparis.DurumId = 5;
				}
				_context.Update(siparis);
			}

			if (teslim != 1)
			{
				SiparisDurum siparisDurum = new SiparisDurum
				{
					Siparis = siparis,
					DurumId = 2,
					Tarih = DateTime.Now
				};

			}
			else
			{
				SiparisDurum siparisDurum = new SiparisDurum
				{
					Siparis = siparis,
					DurumId = 5,
					Tarih = DateTime.Now
				};

				_context.Add(siparisDurum);
			}


			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Siparisİptal(int id)
		{
			var siparis = _context.Siparisler.FirstOrDefault(x => x.Id == id);

			if (siparis != null)
			{
				siparis.DurumId = 7;
				_context.SaveChanges();
			}

			var masa = _context.Masalar.FirstOrDefault(x => x.Id == siparis.MasaId);
			if (masa != null)
			{
				masa.Durum = 1;
				_context.Update(masa);
			}

			SiparisDurum siparisDurum = new SiparisDurum
			{
				SiparisId = id,
				DurumId = 7,
				Tarih = DateTime.Now
			};

			_context.Add(siparisDurum);
            _context.SaveChanges();

            return RedirectToAction("Index");
		}
	}
}
