using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MalzemeController : Controller
	{
		private readonly IdentityDataContext _context;
		public MalzemeController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			ViewBag.Malzemeler = _context.Malzemeler.Include(x => x.Stok).ToList();

			return View();
		}

		[HttpPost]
		public IActionResult Index(Malzeme model)
		{
			ViewBag.Malzemeler = _context.Malzemeler.ToList();

			if (ModelState.IsValid)
			{

				if (model.Id == 0)
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

		public IActionResult MalzemeGuncelle(int Id)
		{
			ViewBag.Kategoriler = _context.Kategoriler.ToList();
			var urun = _context.Malzemeler.FirstOrDefault(x => x.Id == Id);
			return View(urun);
		}

		[HttpPost]
		public IActionResult MalzemeGuncelle(Malzeme model)
		{
			ViewBag.Kategoriler = _context.Kategoriler.ToList();
			_context.Update(model);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult MalzemeSil(int Id)
		{
			var urun = _context.Malzemeler.FirstOrDefault(x => x.Id == Id);
			if (urun != null)
			{
				if (urun.Gorunurluk == true)
				{
					urun.Gorunurluk = false;
				}
				else
				{
					urun.Gorunurluk = true;
				}
				_context.Update(urun);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}
