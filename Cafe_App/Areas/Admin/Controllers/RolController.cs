using Cafe_App.Areas.Admin.Models;
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
			var personelListesi = _context.Personeller
				.Join(_context.Roller,
					  personel => personel.RolId,
					  rol => rol.Id,
					  (personel, rol) => new
					  {
						  RolId = personel.RolId,
						  RolAdi = rol.Ad,
						  Personel = personel
					  })
				.GroupBy(p => p.RolId)
				.Select(g => new AnonimTip
				{
					RolId = g.Key,
					RolAdi = g.First().RolAdi,
					PersonelListesi = g.Select(p => p.Personel).ToList()
				})
				.ToList();

			var viewModel = new RolViewModel
			{
				Rol = new Rol(),
				Roller = _context.Roller.ToList(),
				Personeller = personelListesi // `Personeller`'i uygun şekilde ayarlayın
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index(RolViewModel model) // Rol Ekle Modal
		{
			if (model.Rol.Id == 0)
			{
				var rol = _context.Roller.FirstOrDefault(x => x.Ad == model.Rol.Ad);
				if (rol == null)
				{
					_context.Roller.Add(model.Rol);
				}
				else
				{
					// Varolan Ad Hata	
				}
			}
			else
			{
				_context.Update(model.Rol);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult RolSil(int Id)
		{
			var rol = _context.Roller.FirstOrDefault(x => x.Id == Id);
			if (rol != null)
			{
				if (rol.Gorunurluk == true)
				{
					rol.Gorunurluk = false;
				}
				else
				{
					rol.Gorunurluk = true;
				}
				_context.Update(rol);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		public IActionResult RolGuncelle(int Id) // MODAL YAPILABİLİR
		{
			var rol = _context.Roller.FirstOrDefault(x => x.Id == Id);

			return View(rol);
		}

		[HttpPost]
		public IActionResult RolGuncelle(Rol model)
		{
			_context.Update(model);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult PersonelSil(int Id)
		{
			var personel = _context.Personeller.FirstOrDefault(x => x.Id == Id);
			if (personel != null)
			{
				if (personel.Gorunurluk == true)
				{
					personel.Gorunurluk = false;
				}
				else
				{
					personel.Gorunurluk = true;
				}
				_context.Update(personel);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}
