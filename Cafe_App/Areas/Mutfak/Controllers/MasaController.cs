using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Mutfak.Controllers
{
	[Area("Mutfak")]
	public class MasaController : Controller
	{
		private readonly IdentityDataContext _context;
		public MasaController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new MasaViewModel
			{
				Masa = new Masa(),
				Kategori = new Kategori(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Masa" && x.Gorunurluk == true).ToList(),
				MasaOzellikler = _context.MasaOzellikler.Where(x => x.Gorunurluk == true).ToList(),
				Ozellikler = _context.Ozellikler.Where(x => x.Gorunurluk == true).ToList(),

				Masalar = _context.Masalar
					.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisMenuler).ThenInclude(x => x.Menu)
					.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisUrunler).ThenInclude(x => x.Urun)
					.Include(x => x.MasaOzellikler).ThenInclude(x => x.Ozellik)
					.Include(x => x.Kategori).ToList()
			};

			return View(viewModel);
		}
	}
}
