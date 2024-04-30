using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class StokController : Controller
	{
		private readonly IdentityDataContext _context;
		public StokController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new StokViewModel
			{
				StokGirdi = new StokGirdi(),
				Tedarikci = new Tedarikci(),
				Malzeme = new Malzeme(),
				StokGirdiler = _context.StokGirdiler.Include(x => x.Tedarikci).Include(x => x.Malzeme).OrderByDescending(x => x.Id).ToList(),
				Stoklar = _context.Stoklar.Include(x => x.Malzeme).ToList(),
				Malzemeler = _context.Malzemeler.ToList(),
				Tedarikciler = _context.Tedarikciler.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(StokViewModel model)
		{
			if (model.StokGirdi != null)
			{
				var stok = _context.Stoklar.OrderByDescending(x => x.Id).FirstOrDefault(x => x.MalzemeId == model.StokGirdi.MalzemeId);
				var sonStok = _context.StokGirdiler.OrderByDescending(x => x.Id).FirstOrDefault(x => x.MalzemeId == model.StokGirdi.MalzemeId);

				if (sonStok != null)
				{
					model.StokGirdi.SonStokMiktari = stok.Miktar;
					_context.Update(model.StokGirdi);
				}

				if (stok != null)
				{
					stok.Miktar += model.StokGirdi.Miktar;
					_context.Update(stok);
				}

				_context.Add(model.StokGirdi);
			}
			else if (model.Tedarikci != null)
			{
				_context.Add(model.Tedarikci);
			}
			else if (model.Malzeme != null)
			{
				_context.Add(model.Malzeme);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult StokGirdiGuncelle(StokViewModel model)
		{
			if (model.StokGirdi != null)
			{
				// Güncellenecek stok girdisi ve stok kaydını bul
				var stokGirdiDegisen = _context.StokGirdiler.FirstOrDefault(x => x.Id == model.StokGirdi.Id);
				var stokDegisen = _context.Stoklar.FirstOrDefault(x => x.MalzemeId == model.StokGirdi.MalzemeId);

				// Stok ve stok girdi güncellemesini yap
				if (stokGirdiDegisen != null && stokDegisen != null)
				{
					// Stok miktarını eski stok girdisini çıkartarak güncelle
					stokDegisen.Miktar -= stokGirdiDegisen.Miktar;

					// Stok girdi bilgilerini güncelle
					stokGirdiDegisen.MalzemeId = model.StokGirdi.MalzemeId;
					stokGirdiDegisen.TedarikciId = model.StokGirdi.TedarikciId;
					stokGirdiDegisen.Miktar = model.StokGirdi.Miktar;
					stokGirdiDegisen.AlısFiyati = model.StokGirdi.AlısFiyati;

					// Stok miktarını yeni stok girdisini ekleyerek güncelle
					stokDegisen.Miktar += model.StokGirdi.Miktar;
					

					// Güncellemeleri veritabanında kaydet
					_context.Update(stokGirdiDegisen);
					_context.Update(stokDegisen);
				}

				// Değişiklikleri veritabanına kaydet
				_context.SaveChanges();
			}

			// Güncellenmiş model ile Index sayfasına yönlendir
			return RedirectToAction("Index");
		}


	}
}
