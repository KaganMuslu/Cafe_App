using Cafe_App.Areas.Admin.Models;
using Cafe_App.Areas.Garson.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Garson.Controllers
{
	[Area("Garson")]
	public class OnaylanacakController : Controller
	{
		private readonly IdentityDataContext _context;
		public OnaylanacakController(IdentityDataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var viewModel = new SiparislerModelView
			{
				OnaysizSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => x.DurumId == 1 && x.Gorunurluk == true).ToList(),
				GecmisSiparisler = _context.Siparisler.Include(x => x.Masa).Where(x => x.DurumId == 3 && x.Gorunurluk == true).ToList()
			};

			return View(viewModel);
		}
	}
}
