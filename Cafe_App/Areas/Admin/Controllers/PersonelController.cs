using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class PersonelController : Controller
	{
        private readonly IdentityDataContext _context;
        public PersonelController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			var viewModel = new PersonelViewModel
			{
				Personel = new Personel(),
				Rol= new Rol(),
				Roller = _context.Roller.Where(x => x.Gorunurluk == true).ToList(),
			    Personeller = _context.Personeller.Include(x => x.Rol).ToList()
		    };

			return View(viewModel);
		}

        [HttpPost]
        public async Task<IActionResult> Index(PersonelViewModel model, IFormFile? file)
        {
            if (model.Personel != null)
            {
				if (file != null)
				{
					var uzanti = new[] { ".jpg", ".jpeg", ".png" };
					var resimuzanti = Path.GetExtension(file.FileName);
					if (!uzanti.Contains(resimuzanti))
					{
						ModelState.AddModelError("PersonelFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
					}

					var random = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
					var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", random);
					using (var stream = new FileStream(resimyolu, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
					model.Personel.Fotograf = random;
				}
				else
				{
					if (model.Personel.Id != 0)
					{
						model.Personel.Fotograf = _context.Personeller
							.Where(x => x.Id == model.Personel.Id)
							.Select(x => x.Fotograf)
							.FirstOrDefault();
					}
				}

				if (model.Personel.Id == 0)
				{
					_context.Add(model.Personel);
				}
				else
				{
					_context.Update(model.Personel);
				}
			}
			else if (model.Rol != null)
			{
				var rol = _context.Roller.FirstOrDefault(x => x.Ad == model.Rol.Ad);
				if (rol != null)
				{
					rol.Gorunurluk = true;
					_context.Update(rol);
				}
				else
				{
					_context.Roller.Add(model.Rol);
				}
			}

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

		[AcceptVerbs("GET", "POST")]
		public IActionResult PersonelKontrol(PersonelViewModel model)
		{
			var messages = new List<string>();

			if (model.Personel.DogumTarihi >= DateOnly.FromDateTime(DateTime.Now))
			{
				messages.Add("Doğum tarihi geçmiş tarih olmalıdır.");
			}

			var personelEposta = _context.Personeller.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Eposta == model.Personel.Eposta);
			if (personelEposta != null && personelEposta.Id != model.Personel.Id)
			{
				messages.Add("Bu E-Posta ile daha önce kayıt oluşturulmuştur.");
			}

			var personelTelefon = _context.Personeller.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Telefon == model.Personel.Telefon);
			if (personelTelefon != null && personelTelefon.Id != model.Personel.Id)
			{
				messages.Add("Bu telefon numarası ile daha önce kayıt oluşturulmuştur.");
			}

			// Toplu olarak döndür
			if (messages.Any())
			{
				return Json(messages);
			}

			return Json(true);
		}


		[AcceptVerbs("GET", "POST")]
		public IActionResult RolAdKontrol(RolViewModel model)
		{
			var rol = _context.Roller.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Ad == model.Rol.Ad);

			if (rol != null)
			{
				return Json("Bu isimde bir rol bulunmaktadır.");
			}

			return Json(true);
		}

	}
}
