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
			ViewBag.StokGirdi = _context.StokGirdiler.Include(x => x.Tedarikci).Include(x => x.Malzeme).ToList();
			ViewBag.Malzemeler = _context.Malzemeler.ToList();
			ViewBag.Tedarikciler = _context.Tedarikciler.ToList();


			return View();
		}
	}
}
