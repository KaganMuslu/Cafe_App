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
			ViewBag.StokGirdi = _context.StokGirdiler.Include(x => x.Tedarikci).Include(x => x.Malzeme).OrderByDescending(x => x.Id).ToList();
			ViewBag.Stok = _context.Stoklar.Include(x => x.Malzeme).ToList();
			ViewBag.Malzemeler = _context.Malzemeler.ToList();
			ViewBag.Tedarikciler = _context.Tedarikciler.ToList();


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(StokGirdi model)
		{
			if (ModelState.IsValid)
			{
				var stok = _context.Stoklar.OrderByDescending(x => x.Id).FirstOrDefault(x => x.MalzemeId == model.MalzemeId);
				var sonStok = _context.StokGirdiler.OrderByDescending(x => x.Id).FirstOrDefault(x => x.MalzemeId == model.MalzemeId);

				if (sonStok != null)
				{
					model.SonStokMiktari = stok.Miktar;
					_context.Update(model);
				}

				if (stok != null)
				{
					stok.Miktar += model.Miktar;
					_context.Update(stok);
				}

				_context.Add(model);
				_context.SaveChanges();

			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult StokGirdiGuncelle(StokGirdi model)
		{
			if (ModelState.IsValid)
			{
				// Güncellenecek stok girdisi ve stok kaydını bul
				var stokGirdiDegisen = _context.StokGirdiler.FirstOrDefault(x => x.Id == model.Id);
				var stokDegisen = _context.Stoklar.FirstOrDefault(x => x.MalzemeId == model.MalzemeId);

				// Stok ve stok girdi güncellemesini yap
				if (stokGirdiDegisen != null && stokDegisen != null)
				{
					// Stok miktarını eski stok girdisini çıkartarak güncelle
					stokDegisen.Miktar -= stokGirdiDegisen.Miktar;

					// Stok girdi bilgilerini güncelle
					stokGirdiDegisen.MalzemeId = model.MalzemeId;
					stokGirdiDegisen.TedarikciId = model.TedarikciId;
					stokGirdiDegisen.Miktar = model.Miktar;
					stokGirdiDegisen.AlısFiyati = model.AlısFiyati;

					// Stok miktarını yeni stok girdisini ekleyerek güncelle
					stokDegisen.Miktar += model.Miktar;
					

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
