using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
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
			ViewBag.Masalar = _context.Masalar.Include(x => x.Kategori).ToList();
			
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
	}
}
