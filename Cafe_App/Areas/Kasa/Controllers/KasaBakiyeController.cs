using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Kasa.Controllers
{
	[Area("Kasa")]
	public class KasaBakiyeController : Controller
	{
		private readonly IdentityDataContext _context;
		public KasaBakiyeController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var kasa = _context.Kasalar.FirstOrDefault(x => x.Id == 1);

			return View(kasa);
		}
	}
}
