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
			ViewBag.Roller = _context.Roller.ToList();
			ViewBag.personelRolGrup = _context.Personeller
									.Join(_context.Roller, // Roller tablosu ile birleştirme
										personel => personel.RolId, // Personel tablosundaki RolId alanı
										rol => rol.Id, // Rol tablosundaki Id alanı
										(personel, rol) => new {
											RolId = personel.RolId,
											RolAdi = rol.Ad,
											Personel = personel
										}) // Personel ve Rol tablolarının birleştirilmesi
									.GroupBy(p => p.RolId)
									.Select(g => new
									{
										RolId = g.Key,
										RolAdi = g.First().RolAdi, // Grubun ilk elemanının rol adı
										PersonelListesi = g.Select(p => p.Personel).ToList()
									})
									.ToList();

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
