using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Cafe_App.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class MenuController : Controller
    {
        private readonly IdentityDataContext _context;
        public MenuController(IdentityDataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            var pageSize = 8;
            var menuler = _context.Menuler.Include(x => x.Kategori).ToPagedList(page, pageSize);

            var totalCount = _context.Urunler.Count(); // Toplam menü sayısı
            var startCount = (page - 1) * pageSize + 1; // Başlangıç sayısı
            var endCount = Math.Min(startCount + pageSize - 1, totalCount); // Bitiş sayısı

            ViewBag.TotalCount = totalCount;
            ViewBag.StartCount = startCount;
            ViewBag.EndCount = endCount;

            return View(menuler);
        }
    }
}
