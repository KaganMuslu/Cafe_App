using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class SiparisKasaViewComponent : ViewComponent
{
	private readonly IdentityDataContext _context;

	public SiparisKasaViewComponent(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var onaysizSiparisler = await _context.Siparisler.Where(x => x.DurumId == 5 && x.Gorunurluk == true).CountAsync();

		return View(onaysizSiparisler);
	}
}
