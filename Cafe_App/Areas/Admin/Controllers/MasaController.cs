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
			ViewBag.Kategoriler = _context.Kategoriler.Where(x => x.Tur == "Masa").ToList();
			ViewBag.Masalar = _context.Masalar
				.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisMenuler).ThenInclude(x => x.Menu)
				.Include(x => x.MasaSipariss).ThenInclude(x => x.Siparis).ThenInclude(x => x.SiparisUrunler).ThenInclude(x => x.Urun)
				.Include(x => x.Kategori).ToList();
			
			return View();
		}

		[HttpPost]
		public IActionResult Index(Masa model)
		{
			if (ModelState.IsValid)
			{
				var masa = _context.Masalar.FirstOrDefault(x => x.Kod == model.Kod);
				if (masa == null)
				{
					string masaKod = model.Kod;
					string QrLink = $"https://zartzurt.com/{masaKod}";

					// QR kodu oluştur
					QRCodeGenerator qrGenerator = new QRCodeGenerator();
					QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrLink, QRCodeGenerator.ECCLevel.Q);
					PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
					byte[] qrCodeImage = qrCode.GetGraphic(20);

					// QR kodunu wwwroot/img klasörüne kaydet
					string fileName = $"{model.Kod}.png"; // QR kodunun dosya adı olarak model.Kod kullanılıyor
					string filePath = Path.Combine("wwwroot/img", fileName); // Dosya yolunu oluştur
					System.IO.File.WriteAllBytes(filePath, qrCodeImage); // QR kodunu dosyaya kaydet

					// Modelin QR sütununa dosya yolunu ekleyin
					model.QR = $"/img/{fileName}";

					// Modeli veritabanına ekleyin ve değişiklikleri kaydedin
					_context.Masalar.Add(model);
					_context.SaveChanges();
				}
				else
				{
					// Hata	
				}
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

		public IActionResult MasaGuncelle(Masa model)
		{
			var oldMasa = _context.Masalar.FirstOrDefault(x => x.Id == model.Id);
			if (ModelState.IsValid && oldMasa != null)
			{ 
				if (model.Kod == oldMasa.Kod)
				{
					_context.Update(model);
				}
				else
				{
					string masaKod = model.Kod;
					string QrLink = $"https://zartzurt.com/{masaKod}";

					// QR kodu oluştur
					QRCodeGenerator qrGenerator = new QRCodeGenerator();
					QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrLink, QRCodeGenerator.ECCLevel.Q);
					PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
					byte[] qrCodeImage = qrCode.GetGraphic(20);

					// QR kodunu wwwroot/img klasörüne kaydet
					string fileName = $"{model.Kod}.png"; // QR kodunun dosya adı olarak model.Kod kullanılıyor
					string filePath = Path.Combine("wwwroot/img", fileName); // Dosya yolunu oluştur
					System.IO.File.WriteAllBytes(filePath, qrCodeImage); // QR kodunu dosyaya kaydet

					// Modelin QR sütununa dosya yolunu ekleyin
					model.QR = $"/img/{fileName}";


					oldMasa.Kod = model.Kod;
					oldMasa.QR = model.QR;
					oldMasa.KategoriId = model.KategoriId;
					oldMasa.Kapasite = model.Kapasite;

					_context.Update(oldMasa);
				}
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
