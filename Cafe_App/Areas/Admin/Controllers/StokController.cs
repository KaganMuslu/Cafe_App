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
				var stokGirdiDegisen = _context.StokGirdiler.FirstOrDefault(x => x.Id == model.Id);
				var stokDegisen = _context.Stoklar.FirstOrDefault(x => x.MalzemeId == stokGirdiDegisen.MalzemeId);

				if (stokDegisen != null && stokGirdiDegisen != null)
				{
					stokDegisen.Miktar -= stokGirdiDegisen.Miktar;
					stokGirdiDegisen.Miktar = model.Miktar;
					stokGirdiDegisen.TedarikciId = model.TedarikciId;
					stokGirdiDegisen.MalzemeId= model.MalzemeId;
					stokGirdiDegisen.AlısFiyati = model.;
				}

				_context.Update(stokDegisen);
				_context.Update(stokGirdiDegisen);
				_context.SaveChanges();

			}

			return View("Index", model);
		}

	}
}
