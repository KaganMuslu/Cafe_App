using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Mutfak.Controllers
{
	[Area("Mutfak")]
	public class StokController : Controller
	{
		private readonly IdentityDataContext _context;
		public StokController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new StokMutfakViewModel
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
	}
}
