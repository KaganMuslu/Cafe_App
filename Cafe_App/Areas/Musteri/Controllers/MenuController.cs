using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            var menuler = _context.Menuler.Include(x => x.Kategori).ToList();

            return View(menuler);
        }
    }
}
