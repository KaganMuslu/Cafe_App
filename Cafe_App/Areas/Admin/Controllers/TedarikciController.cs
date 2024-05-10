using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class TedarikciController : Controller
	{
        private readonly IdentityDataContext _context;
        public TedarikciController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
            ViewBag.Tedarikciler = _context.Tedarikciler.ToList();
			return View();
		}

        [HttpPost]
        public IActionResult Index(Tedarikci model)
        {
			if (ModelState.IsValid)
			{
				var tedarikci = _context.Tedarikciler.FirstOrDefault(x => x.Eposta == model.Eposta);
				if (tedarikci == null)
				{
					_context.Tedarikciler.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

            ViewBag.Tedarikciler = _context.Tedarikciler.ToList();
			return RedirectToAction("Index");
		}

		public IActionResult TedarikciSil(int Id)
		{
			var tedarikci = _context.Tedarikciler.FirstOrDefault(x => x.Id == Id);
			if (tedarikci != null)
			{
				if (tedarikci.Gorunurluk == true)
				{
					tedarikci.Gorunurluk = false;
				}
				else
				{
					tedarikci.Gorunurluk = true;
				}
				_context.Update(tedarikci);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult TedarikciKontrol(Tedarikci model)
		{
			var messages = new List<string>();

			var tedarikciFirma = _context.Tedarikciler.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Firma == model.Firma);
			if (tedarikciFirma != null)
			{
				messages.Add("Bu firma ile daha önce kayıt oluşturulmuştur.");
			}

			var tedarikciEposta = _context.Tedarikciler.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Eposta == model.Eposta);
			if (tedarikciEposta != null)
			{
				messages.Add("Bu E-Posta ile daha önce kayıt oluşturulmuştur.");
			}

			var tedarikciTelefon = _context.Personeller.Where(x => x.Gorunurluk == true).FirstOrDefault(x => x.Telefon == model.Telefon);
			if (tedarikciTelefon != null)
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

	}
}
