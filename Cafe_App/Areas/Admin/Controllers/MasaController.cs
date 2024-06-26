﻿using Cafe_App.Areas.Admin.Models;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class MasaController : Controller
	{
		private readonly IdentityDataContext _context;
		public MasaController(IdentityDataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var viewModel = new MasaViewModel
			{
				Masa = new Masa(),
				Kategori = new Kategori(),
				Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Masa" && x.Gorunurluk == true).ToList(),
				MasaOzellikler = _context.MasaOzellikler.Where(x => x.Gorunurluk == true).ToList(),
				Ozellikler = _context.Ozellikler.Where(x => x.Gorunurluk == true).ToList(),

				Masalar = _context.Masalar
					.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisMenuler).ThenInclude(x => x.Menu)
					.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisUrunler).ThenInclude(x => x.Urun)
					.Include(x => x.MasaOzellikler).ThenInclude(x => x.Ozellik)
					.Include(x => x.Kategori).ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index(MasaViewModel model)
		{
			if (model.Masa != null)
			{
				var masa = _context.Masalar.FirstOrDefault(x => x.Kod == model.Masa.Kod);
				if (masa == null)
				{
					model.Masa.Link = new string(Enumerable.Range(0, 6).Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[new Random().Next(36)]).ToArray());
					string masaLink = model.Masa.Link;
					string QrLink = $"http://192.168.1.132:5000/Musteri/Urun?masa={masaLink}";

					// QR kodu oluştur
					QRCodeGenerator qrGenerator = new QRCodeGenerator();
					QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrLink, QRCodeGenerator.ECCLevel.Q);
					PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
					byte[] qrCodeImage = qrCode.GetGraphic(20);

					// QR kodunu wwwroot/img klasörüne kaydet
					string fileName = $"{model.Masa.Kod}.png"; // QR kodunun dosya adı olarak model.Kod kullanılıyor
					string filePath = Path.Combine("wwwroot/img", fileName); // Dosya yolunu oluştur
					System.IO.File.WriteAllBytes(filePath, qrCodeImage); // QR kodunu dosyaya kaydet

					// Modelin QR sütununa dosya yolunu ekleyin
					model.Masa.QR = $"/img/{fileName}";

					// Modeli veritabanına ekleyin ve değişiklikleri kaydedin
					_context.Masalar.Add(model.Masa);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}
			else if (model.Kategori != null)
			{
				var kategori = _context.Kategoriler.FirstOrDefault(x => x.Ad == model.Kategori.Ad);
				if (kategori == null)
				{
					_context.Kategoriler.Add(model.Kategori);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
			}

			return RedirectToAction("Index");
		}

		public IActionResult MasaSil(int Id)
		{
			var masa = _context.Masalar.FirstOrDefault(x => x.Durum == 1 && x.Id == Id);
			if (masa != null)
			{
				if (masa.Gorunurluk == true)
				{
					masa.Gorunurluk = false;
				}
				else
				{
					masa.Gorunurluk = true;
				}
				_context.Update(masa);
				_context.SaveChanges();
				
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult MasaBilgiler(int masaId)
		{
			// masaId'ye göre sorgulama işlemi yapın
			var masaData = GetMasaDataById(masaId);

			if (masaData == null)
			{
				// Masa verisi bulunamadığında NotFound döndür
				return NotFound();
			}

			// Verileri JSON formatında geri döndür
			return Json(masaData);
		}

		// masaId'ye göre sorgulama fonksiyonu
		private Masa GetMasaDataById(int masaId)
		{
			var masaData = _context.Masalar.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).FirstOrDefault(x => x.Id == masaId);
			if (masaData == null)
			{
				// Hatax
			}
			else
			{
				var masaSiparisler = masaData.MasaSipariss.ToList();
			}
			return masaData; // Fonksiyonun türü MasaData olmalı
		}

		public IActionResult MasaGuncelle(MasaViewModel model)
		{
			var oldMasa = _context.Masalar.FirstOrDefault(x => x.Id == model.Masa.Id);
			if (oldMasa != null)
			{ 
				if (model.Masa.Kod == oldMasa.Kod)
				{
					oldMasa.Kategori = model.Masa.Kategori;
					oldMasa.Kapasite = model.Masa.Kapasite;
					_context.Update(oldMasa);
				}
				else
				{
                    model.Masa.Link = new string(Enumerable.Range(0, 6).Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[new Random().Next(36)]).ToArray());
                    string masaLink = model.Masa.Link;
                    string QrLink = $"http://192.168.1.132:5000/Musteri/Urun?masa={masaLink}";

					// QR kodu oluştur
					QRCodeGenerator qrGenerator = new QRCodeGenerator();
					QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrLink, QRCodeGenerator.ECCLevel.Q);
					PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
					byte[] qrCodeImage = qrCode.GetGraphic(20);

					// QR kodunu wwwroot/img klasörüne kaydet
					string fileName = $"{model.Masa.Kod}.png"; // QR kodunun dosya adı olarak model.Kod kullanılıyor
					string filePath = Path.Combine("wwwroot/img", fileName); // Dosya yolunu oluştur
					System.IO.File.WriteAllBytes(filePath, qrCodeImage); // QR kodunu dosyaya kaydet

					// Modelin QR sütununa dosya yolunu ekleyin
					model.Masa.QR = $"/img/{fileName}";


					oldMasa.Link = model.Masa.Link;
					oldMasa.Kod = model.Masa.Kod;
                    oldMasa.QR = model.Masa.QR;
					oldMasa.KategoriId = model.Masa.KategoriId;
					oldMasa.Kapasite = model.Masa.Kapasite;

					_context.Update(oldMasa);
				}
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult MasaOzellikSec(MasaViewModel model)
		{
			if (model.SecilenOzellikler != null)
			{
				var masaOzelliklerEski = _context.MasaOzellikler.Where(x => x.MasaId == model.Masa.Id).ToList();
				foreach (var eskOzellik in masaOzelliklerEski)
				{
					_context.Remove(eskOzellik);
				}

				foreach (var ozellikId in model.SecilenOzellikler)
				{
					var masaOzellik = new MasaOzellik
					{
						OzellikId = ozellikId,
						MasaId = model.Masa.Id,
						Gorunurluk = true
					};

					_context.Add(masaOzellik);
					
				}

				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}


		[AcceptVerbs("GET", "POST")]
		public IActionResult MasaKontrol(MasaViewModel model)
		{
			var messages = new List<string>();

			var masaKod = _context.Masalar.FirstOrDefault(x => x.Kod == model.Masa.Kod && x.Gorunurluk == true && x.Id != model.Masa.Id);
			if (masaKod != null)
			{
				messages.Add("Bu kod ile daha önce masa oluşturulmuştur.");
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
