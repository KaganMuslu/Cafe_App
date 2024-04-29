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
				Roller = _context.Roller.ToList(),
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
						ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
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
					else
					{
						ModelState.AddModelError("OgrenciFotograf", "Fotoğraf boş olamaz");
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
				_context.Roller.Add(model.Rol);
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

	}
}
