﻿using Cafe_App.Areas.Admin.Models;
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
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult MalzemeSil(int Id)
		{
			var malzeme = _context.Malzemeler.FirstOrDefault(x => x.Id == Id);
			if (malzeme != null)
			{
				if (malzeme.Gorunurluk == true)
				{
					malzeme.Gorunurluk = false;
					var malzemeUrunler = _context.UrunMalzemeler.Where(x => x.MalzemeId == Id).ToList();
					foreach (var malzemeUrun in malzemeUrunler)
					{
						malzemeUrun.Gorunurluk = false;
					}
					var stokGirdiMalzemeler = _context.StokGirdiler.Where(x => x.MalzemeId == Id).ToList();
					foreach (var stokGirdiMalzeme in stokGirdiMalzemeler)
					{
						stokGirdiMalzeme.Gorunurluk = false;
					}
				}
				else
				{
					malzeme.Gorunurluk = true;
					var malzemeUrunler = _context.UrunMalzemeler.Where(x => x.MalzemeId == Id).ToList();
					foreach (var malzemeUrun in malzemeUrunler)
					{
						malzemeUrun.Gorunurluk = true;
					}
					var stokGirdiMalzemeler = _context.StokGirdiler.Where(x => x.MalzemeId == Id).ToList();
					foreach (var stokGirdiMalzeme in stokGirdiMalzemeler)
					{
						stokGirdiMalzeme.Gorunurluk = true;
					}
				}
				_context.Update(malzeme);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult MalzemeKontrol(Malzeme model)
		{
			var messages = new List<string>();

			var urunAd = _context.Malzemeler.FirstOrDefault(x => x.Ad == model.Ad && x.Gorunurluk == true && x.Id != model.Id);

			if (urunAd != null)
			{
				messages.Add("Bu ada sahip bir malzeme bulunmaktadır.");
			}

			if (model.Stok?.MinStok != null && model.Stok?.MaxStok != null)
			{
				if (model.Stok.MinStok > model.Stok.MaxStok)
				{
					messages.Add("Maximum stok minimum stoktan büyük olmalıdır.");
				}
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
