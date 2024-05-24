using Cafe_App.Areas.Garson.Models;
using Cafe_App.Data;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Kasa.Controllers
{
	[Area("Kasa")]
	public class SiparisKontrolController : Controller
	{
		private readonly IdentityDataContext _context;
		public SiparisKontrolController(IdentityDataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var viewModel = new SiparislerModelView()
			{
				OnaysizSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => x.DurumId == 5 && x.Gorunurluk == true).ToList(),
				GecmisSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => x.DurumId != 5 && x.Gorunurluk == true).ToList(),
				SiparisUrunler = _context.SiparisUrunler.Include(x => x.Urun).ToList(),
				SiparisMenuler = _context.SiparisMenuler.Include(x => x.Menu).ToList()

			};

			List<SiparisDurum> siparisDurumlar = [];
			foreach (var siparis in _context.Siparisler.Where(x => x.Gorunurluk == true).ToList())
			{
				var siparisDurum = _context.SiparisDurumlar.Include(x => x.Durum).OrderByDescending(x => x.Id).FirstOrDefault(x => x.SiparisId == siparis.Id);
				if (siparisDurum != null)
				{
					siparisDurumlar.Add(siparisDurum);
				}
			}

			viewModel.SiparisDurumlar = siparisDurumlar;


			return View(viewModel);
		}

		public IActionResult SiparisOnayla(int id)
		{
			var siparis = _context.Siparisler.FirstOrDefault(x => x.Id == id);
			if(siparis != null)
			{
				siparis.DurumId = 6;
				siparis.OdemeDurum = true;
				siparis.OdenenTutar = siparis.Tutar;

				SiparisDurum siparisDurum = new SiparisDurum
				{
					Siparis = siparis,
					DurumId = 6,
					Tarih = DateTime.Now
				};

				var masa = _context.Masalar.FirstOrDefault(x => x.Id == siparis.MasaId);
				if (masa != null)
				{
					masa.Durum = 1;
					_context.Update(masa);
				}

				var kasa = _context.Kasalar.FirstOrDefault(x => x.Id == 1);
				if (kasa != null)
				{
					kasa.Bakiye += siparis.Tutar;
					_context.Update(kasa);
				}

				_context.Update(siparis);
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

			SiparisDurum siparisDurum = new SiparisDurum
			{
				SiparisId = id,
				DurumId = 7,
				Tarih = DateTime.Now
			};

			var masa = _context.Masalar.FirstOrDefault(x => x.Id == siparis.MasaId);
			if (masa != null)
			{
				masa.Durum = 1;
				_context.Update(masa);
			}

			_context.Add(siparisDurum);
            _context.SaveChanges();

            return RedirectToAction("Index");
		}
	}
}
