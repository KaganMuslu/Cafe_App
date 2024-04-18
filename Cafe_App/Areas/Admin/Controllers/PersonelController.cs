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
            var personeller = _context.Personeller.Include(x => x.Rol).ToList();
			return View(personeller);
		}

		public IActionResult PersonelEkle(int Id)
		{
			ViewBag.roller = _context.Roller.ToList();

            if (Id == 0)
            {
                return View();
			}

            var personel = _context.Personeller.FirstOrDefault(x => x.Id == Id);
			return View(personel);
		}

        [HttpPost]
        public async Task<IActionResult> PersonelEkle(Personel model, IFormFile? file)
        {
			ViewBag.roller = _context.Roller.ToList();
			ViewBag.Kategoriler = _context.Kategoriler.ToList();

			if (ModelState.IsValid && model.RolId != 0)
            {
                
                if (file != null)
                {
                    var uzanti = new[] { ".jpg", ".jpeg", ".png" };
                    var resimuzanti = Path.GetExtension(file.FileName);
                    if (!uzanti.Contains(resimuzanti))
                    {
                        ModelState.AddModelError("OgrenciFotograf", "Geçerli bir fotoğraf formatı seçiniz. *jpg,jpeg,png");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("OgrenciFotograf", "Fotoğraf boş olamaz");
                    return View(model);
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

                return RedirectToAction("Index");
            }

            return View(model);
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
