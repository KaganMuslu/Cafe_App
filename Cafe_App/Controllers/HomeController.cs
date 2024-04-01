using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cafe_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityDataContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IdentityDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var kasa = new Kasa { Bakiye = 500 };
            _context.Add(kasa);
            _context.SaveChanges();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
