using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class MusteriController : Controller
	{
		private readonly IdentityDataContext _context;
		public MusteriController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new MusteriViewModel
			{
				Musteri = new Cafe_App.Models.Musteri(),
				Musteriler = _context.Musteriler.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index(MusteriViewModel model)
		{
			var musteri = _context.Musteriler.FirstOrDefault(x => x.Eposta == model.Musteri.Eposta);
			if (musteri == null)
			{
				_context.Musteriler.Add(model.Musteri);
			}
			else
			{
				// Önceki soruguyu untracked yani takipsiz yapma
				var entry = _context.Entry(musteri);
				entry.State = EntityState.Detached;

				_context.Update(model.Musteri);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult MusteriSil(int id)
		{
			var musteri = _context.Musteriler.FirstOrDefault(x => x.Id == id);
			if (musteri != null)
			{
				if (musteri.Gorunurluk == true)
				{
					musteri.Gorunurluk = false;
				}
				else
				{
					musteri.Gorunurluk = true;
				}
				_context.Update(musteri);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}
	}
}
