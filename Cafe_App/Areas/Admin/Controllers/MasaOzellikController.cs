using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MasaOzellikController : Controller
	{
		private readonly IdentityDataContext _context;
		public MasaOzellikController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new MasaOzellikViewModel
			{
				Ozellik = new Ozellik(),
				MasaOzellik = new MasaOzellik(),
				Ozellikler = _context.Ozellikler.ToList(),
				MasaOzellikler = _context.MasaOzellikler.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index(MasaOzellikViewModel model)
		{
			if (model.Ozellik != null)
			{
				if (model.Ozellik.Id == null) // Ekle
				{
					var ozellik = _context.Ozellikler.FirstOrDefault(x => x.Ad == model.Ozellik.Ad);
					if (ozellik == null)
					{
						_context.Add(model.Ozellik);
					}
					else
					{
						// Önceki soruguyu untracked yani takipsiz yapma
						var entry = _context.Entry(ozellik);
						entry.State = EntityState.Detached;

						_context.Update(model.Ozellik);
					}
				}
				else // Güncelle
				{
					_context.Update(model.Ozellik);
				}
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		
		public IActionResult MasaOzellikSil(int Id)
		{
			var ozellik = _context.Ozellikler.FirstOrDefault(x => x.Id == Id);
			if (ozellik != null)
			{
				if (ozellik.Gorunurluk == true)
				{
					ozellik.Gorunurluk = false;
				}
				else
				{
					ozellik.Gorunurluk = true;
				}
				_context.Update(ozellik);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}
