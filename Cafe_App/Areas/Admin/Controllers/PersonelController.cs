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
            ViewBag.personeller = _context.Personeller.Include(x => x.Rol).ToList();
			ViewBag.roller = _context.Roller.ToList();
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> PersonelEkle(Personel model, IFormFile? file)
        {
			if (ModelState.IsValid && model.RolId != 0)
            {
                
                if (file != null)
                {
                    var uzanti = new[] { ".jpg", ".jpeg", ".png" };
                    var resimuzanti = Path.GetExtension(file.FileName);
                    if (!uzanti.Contains(resimuzanti))
                    {
                        ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
                    }
                }
                else
                {
                    ModelState.AddModelError("OgrenciFotograf", "Fotoğraf boş olamaz");
				}

                var random = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
                var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", random);
                using (var stream = new FileStream(resimyolu, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                model.Fotograf = random;


                if(model.Id == 0)
                {
                    _context.Add(model);
                }
                else
                {
                    _context.Update(model);
                }
                _context.SaveChanges();
            }

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
