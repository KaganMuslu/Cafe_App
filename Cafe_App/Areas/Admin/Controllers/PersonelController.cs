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
			return View();
		}

		public IActionResult PersonelEkle()
		{
			ViewBag.roller = _context.Roller.ToList();

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> PersonelEkle(Personel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var personel = _context.Personeller.FirstOrDefault(x => x.Eposta == model.Eposta);
                if (personel == null)
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


                    _context.Add(model);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
