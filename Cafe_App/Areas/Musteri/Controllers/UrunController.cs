using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            var urunler = _context.Urunler.Include(x => x.Kategori).ToList();

            return View(urunler);
        }
    }
}
