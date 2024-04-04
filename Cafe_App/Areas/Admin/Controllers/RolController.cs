using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RolController : Controller
	{
        private readonly IdentityDataContext _context;
        public RolController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Rol model) // Rol Ekle Modal
		{
			if (ModelState.IsValid)
			{
				var rol = _context.Roller.FirstOrDefault(x => x.Ad == model.Ad);
				if (rol == null)
				{
					_context.Roller.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

			return RedirectToAction("Index");
		}
	}
}
