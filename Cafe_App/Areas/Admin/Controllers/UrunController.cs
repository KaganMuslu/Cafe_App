using Cafe_App.Areas.Admin.Data;
using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Cafe_App.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class UrunController : Controller
	{
		private readonly IdentityDataContext _context;
		public UrunController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new UrunViewModel
			{
				Urun = new Urun(),
				Kategori = new Kategori(),
				Urunler = _context.Urunler.Include(x => x.Kategori).ToList(),
				Malzemeler = _context.Malzemeler.Where(x => x.Gorunurluk == true).ToList(),
				UrunMalzemeler = _context.UrunMalzemeler.Include(x => x.Urun).Include(x => x.Malzeme).Where(x => x.Gorunurluk == true).ToList(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Urun").ToList(),
			};

			List<UrunIndirim> UrunIndirimler = [];
			foreach (var urun in _context.Urunler.Where(x => x.Gorunurluk == true).ToList())
			{
				var indirim = _context.UrunIndirimler.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UrunId == urun.Id);
				if (indirim != null)
				{
					UrunIndirimler.Add(indirim);
				}
			}

			viewModel.UrunIndirimler = UrunIndirimler;

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(UrunViewModel model, IFormFile? file)
		{
			if (model.Urun != null)
			{
				if (model.Urun.KategoriId != 0)
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

						var fotografRandom = string.Format($"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");
						var resimyolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fotografRandom);
						using (var stream = new FileStream(resimyolu, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						model.Urun.Fotograf = fotografRandom;
					}
					else
					{
						model.Urun.Fotograf = _context.Urunler.Where(x => x.Id == model.Urun.Id).Select(x => x.Fotograf).FirstOrDefault();
					}

					if (model.Urun.Id == 0)
					{
						_context.Add(model.Urun);
					}
					else
					{
						_context.Update(model.Urun);
					}
				
				}
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult KategoriEkle(Kategori model)
		{
			if (model != null)
			{
				_context.Add(model);
			}
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult MalzemeSecilebilir(int urunId, int malzemeId)
		{
			var urunMalzeme = _context.UrunMalzemeler.FirstOrDefault(x => x.UrunId == urunId && x.MalzemeId == malzemeId);
			if (urunMalzeme != null)
			{
				if (urunMalzeme.Secenek == true)
				{
					urunMalzeme.Secenek = false;
				}
				else
				{
					urunMalzeme.Secenek = true;
				}

				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		public IActionResult UrunSil(int Id)
		{
			var urun = _context.Urunler.FirstOrDefault(x => x.Id == Id);
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

		[HttpPost]
		public IActionResult MalzemeSec(int urunId, string secilenMalzemeler)
		{
			var urun = JsonConvert.DeserializeObject<List<Malzemelers>>(secilenMalzemeler);

			var sorgu = _context.UrunMalzemeler.Where(x => x.UrunId == urunId).ToList();
			foreach (var sorguSil in sorgu)
			{
				_context.Remove(sorguSil);
			}

			foreach (var item in urun)
			{
				var malzemeSec = _context.Malzemeler.FirstOrDefault(x => x.Id == item.MalzemeId);

				if (malzemeSec != null)
				{
					_context.UrunMalzemeler.Add(new UrunMalzeme
					{
						UrunId = urunId,
						MalzemeId = malzemeSec.Id,
						Miktar = int.Parse(item.Miktar),
						Gorunurluk = true
					});
				}
			}

			_context.SaveChanges();

			return RedirectToAction("Index");
		}


		public IActionResult MalzemeSil(int Id, int malzemeId)
		{
			var urunMalzeme = _context.UrunMalzemeler.Where(x => x.UrunId == Id && x.MalzemeId == malzemeId).FirstOrDefault();
			if (urunMalzeme != null)
			{
				urunMalzeme.Gorunurluk = false;

				_context.Update(urunMalzeme);
				_context.SaveChanges();
			}
			// Urune ait malzeme bulunamadı

			return RedirectToAction("Index");
		}

		[AcceptVerbs("GET", "POST")]
		public IActionResult UrunKontrol(UrunViewModel model)
		{
			var messages = new List<string>();

			var urunAd = _context.Urunler.FirstOrDefault(x => x.Ad == model.Urun.Ad && x.Gorunurluk == true && x.Id != model.Urun.Id);

			if (urunAd != null)
			{
				messages.Add("Bu ada sahip bir ürün bulunmaktadır.");
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
