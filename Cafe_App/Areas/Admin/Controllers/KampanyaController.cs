﻿using Microsoft.AspNetCore.Mvc;

namespace Cafe_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KampanyaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
