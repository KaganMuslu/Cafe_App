using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using X.PagedList;

namespace Cafe_App.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class UrunController : Controller
    {
        private readonly IdentityDataContext _context;
        public UrunController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index(string masa, int page = 1)
        {
            var pageSize = 8;
            var urunler = _context.Urunler.Include(x => x.Kategori).ToPagedList(page, pageSize);

            var totalCount = _context.Urunler.Count(); // Toplam ürün sayısı
            var startCount = (page - 1) * pageSize + 1; // Başlangıç sayısı
            var endCount = Math.Min(startCount + pageSize - 1, totalCount); // Bitiş sayısı

            ViewBag.TotalCount = totalCount;
            ViewBag.StartCount = startCount;
            ViewBag.EndCount = endCount;

            var simdikiTarih = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.UrunIndirimler = _context.UrunIndirimler
                .Where(x => x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih)
                .ToList();

            if (masa != null)
            {
                var masaObj = _context.Masalar.FirstOrDefault(x => x.Link == masa);
                if (masaObj != null)
                {
                    ViewBag.Masa = masaObj;
                    HttpContext.Session.SetString("MasaKodu", masaObj.Kod); // Masa kodunu session'a ekliyoruz
                }
            }

            return View(urunler);
        }

        public IActionResult UrunDetay(int id)
        {
            var urun = _context.Urunler.Include(x => x.Kategori).FirstOrDefault(x => x.Id == id);
            if (urun == null)
            {
                return RedirectToAction("Index");
            }

            return View(urun);
        }

    }
}
