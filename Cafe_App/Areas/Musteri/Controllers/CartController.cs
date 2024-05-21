using Cafe_App.Areas.Admin.Data;
using Cafe_App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Cafe_App.Areas.Musteri.Controllers
{
	[Area("Musteri")]
    public class CartController : Controller
    {
		private readonly IdentityDataContext _context;
		public CartController(IdentityDataContext context)
		{
			_context = context;
		}


		private Cart GetCart()
        {
            var cart = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cart) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cart);
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }

        public IActionResult AddToCart(int productId, int menuId, int minus, string url)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.Session.GetString("SessionId"); // güncel kullanıcı id'si
            if (string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("SessionId", userId);
            }

            var cart = GetCart();
            var existingItem = cart.Items.FirstOrDefault(i => i.UrunId == productId && i.MenuId == menuId && i.KullaniciId == userId);

            if (existingItem != null)
            {
                if (minus == 1)
                {
					existingItem.Quantity--;
				}
                else
                {
				    existingItem.Quantity++;
                }
                
                if (productId != 0)
                {
                    var urunAd = _context.Urunler.FirstOrDefault(x => x.Id == productId);
                    TempData["SepeteEklendi"] = urunAd.Ad;
                }
                else
                {
                    var menuAd = _context.Menuler.FirstOrDefault(x => x.Id == menuId);
                    TempData["SepeteEklendi"] = menuAd.Ad;
                }

            }
            else
            {
			    var simdikiTarih = DateOnly.FromDateTime(DateTime.Now);
                if (productId != 0)
                {
                    var indirimliFiyat = _context.UrunIndirimler.OrderByDescending(x => x.Id)
                                                                    .FirstOrDefault(x =>
                                                                    x.UrunId == productId &&
                                                                    x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih);

                    var urun = _context.Urunler.FirstOrDefault(x => x.Id == productId);


                    if (indirimliFiyat != null)
                    {
                        cart.Items.Add(new CartItem
                        {
                            UrunId = productId,
                            MenuId = menuId,
                            Fiyat = indirimliFiyat.YeniFiyat ?? 0,
                            KullaniciId = userId,
                            Urun = _context.Urunler.FirstOrDefault(x => x.Id == productId),
                            Menu = _context.Menuler.FirstOrDefault(x => x.Id == menuId)
                        });
                    }
                    else
                    {

                        cart.Items.Add(new CartItem
                        {
                            UrunId = productId,
                            MenuId = menuId,
                            Fiyat = urun.Fiyat,
                            KullaniciId = userId,
                            Urun = _context.Urunler.FirstOrDefault(x => x.Id == productId),
                            Menu = _context.Menuler.FirstOrDefault(x => x.Id == menuId)
                        });
                    }

                    TempData["SepeteEklendi"] = urun.Ad;

                }
                else
                {
                    var indirimliFiyat = _context.MenuIndirimler.OrderByDescending(x => x.Id)
                                                                   .FirstOrDefault(x =>
                                                                   x.MenuId == menuId &&
                                                                   x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih);

                    var menu = _context.Menuler.FirstOrDefault(x => x.Id == menuId);

                    if (indirimliFiyat != null)
                    {
                        cart.Items.Add(new CartItem
                        {
                            UrunId = productId,
                            MenuId = menuId,
                            Fiyat = indirimliFiyat.YeniFiyat ?? 0,
                            KullaniciId = userId,
                            Urun = _context.Urunler.FirstOrDefault(x => x.Id == productId),
                            Menu = _context.Menuler.FirstOrDefault(x => x.Id == menuId)
                        });
                    }
                    else
                    {
                        

                        cart.Items.Add(new CartItem
                        {
                            UrunId = productId,
                            MenuId = menuId,
                            Fiyat = menu.Fiyat,
                            KullaniciId = userId,
                            Urun = _context.Urunler.FirstOrDefault(x => x.Id == productId),
                            Menu = _context.Menuler.FirstOrDefault(x => x.Id == menuId)
                        });
                    }

                    TempData["SepeteEklendi"] = menu.Ad;
                }
            }

            
            SaveCart(cart);

            if (url != null)
            {
                return Redirect(url);
            }


            return RedirectToAction("Cart"); // Sepete ekledikten sonra yönlendirme

        }

        public IActionResult RemoveCart(int productId, int menuId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.Session.GetString("SessionId"); // güncel kullanıcı id'si
            var cart = GetCart();

			var existingItem = cart.Items.FirstOrDefault(i => i.UrunId == productId && i.MenuId == menuId && i.KullaniciId == userId);
            cart.Items.Remove(existingItem);
            SaveCart(cart);

			return RedirectToAction("Cart");
        }

        public IActionResult Cart(bool GirisYap, bool SiparisAlındı)
        {
			var simdikiTarih = DateOnly.FromDateTime(DateTime.Now);
			ViewBag.UrunIndirimler = _context.UrunIndirimler
				.Where(x => x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih)
				.ToList();

            ViewBag.MenuIndirimler = _context.MenuIndirimler
                .Where(x => x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih)
                .ToList();

            ViewBag.Masa = HttpContext.Session.GetString("MasaKodu");
			var cart = GetCart();


            if (GirisYap == true)
            {
                ViewBag.GirisYap = true;
			}
			if (SiparisAlındı == true)
			{
				ViewBag.SiparisAlındı = true;
			}

			return View(cart);
        }

        public IActionResult SiparisTamamla()
        {
            var masaKodu = HttpContext.Session.GetString("MasaKodu");
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.Session.GetString("SessionId");
			var simdikiTarih = DateOnly.FromDateTime(DateTime.Now);

			if (!string.IsNullOrEmpty(masaKodu) && !string.IsNullOrEmpty(userId))
            {
                var masa = _context.Masalar.FirstOrDefault(x => x.Kod == masaKodu);
                var siprais = new Siparis
                {
                    Tarih = DateTime.Now,
					Kullanıcı = userId,
                    DurumId = 1,
					MasaId = masa.Id,
					Tutar = 0,
					OdenenTutar = 0,
					OdemeDurum = false,
                    Gorunurluk = true,
                };

                _context.Add(siprais);
                _context.SaveChanges();
                siprais = _context.Siparisler.FirstOrDefault(x => x.Tarih == siprais.Tarih && x.AdresId == siprais.AdresId);

				var cart = GetCart();
                foreach (var item in cart.Items)
                {
                    if (item.UrunId != 0)
                    {
                        var indirimliFiyat = _context.UrunIndirimler.OrderByDescending(x => x.Id)
                                                                    .FirstOrDefault(x => 
                                                                    x.UrunId == item.UrunId &&
																	x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih);
                        var SiparisUrun = new SiparisUrun();


						if (indirimliFiyat != null && indirimliFiyat.YeniFiyat != 0)
                        {
							SiparisUrun = new SiparisUrun
							{
								Miktar = item.Quantity,
                                Fiyat = indirimliFiyat.YeniFiyat ?? item.Urun.Fiyat,
								SiparisId = siprais.Id,
								UrunId = item.UrunId,
								Gorunurluk = true
							};

							siprais.Tutar += (item.Quantity * indirimliFiyat.YeniFiyat ?? 0);
                        }
                        else
                        {
							SiparisUrun = new SiparisUrun
							{
								Miktar = item.Quantity,
								Fiyat = item.Urun.Fiyat,
								SiparisId = siprais.Id,
								UrunId = item.UrunId,
								Gorunurluk = true
							};

							siprais.Tutar += (item.Urun.Fiyat * item.Quantity);
						}
                        _context.Add(SiparisUrun);
                    }
                    else
                    {
                        var SiparisMenu = new SiparisMenu();

						var indirimliFiyat = _context.MenuIndirimler.OrderByDescending(x => x.Id)
                                                                    .FirstOrDefault(x =>
                                                                                x.MenuId == item.MenuId &&
																				x.BaslangıcTarihi <= simdikiTarih && x.BitisTarihi >= simdikiTarih);

						if (indirimliFiyat != null && indirimliFiyat.YeniFiyat != 0)
						{
							SiparisMenu = new SiparisMenu
							{
								Miktar = item.Quantity,
								SiparisId = siprais.Id,
                                Fiyat = indirimliFiyat.YeniFiyat ?? item.Menu.Fiyat,
								MenuId = item.MenuId,
								Gorunurluk = true
							};

							siprais.Tutar += (item.Quantity * indirimliFiyat.YeniFiyat ?? 0);
						}
						else
						{
							SiparisMenu = new SiparisMenu
							{
								Miktar = item.Quantity,
								SiparisId = siprais.Id,
								Fiyat = item.Menu.Fiyat,
								MenuId = item.MenuId,
								Gorunurluk = true
							};

							siprais.Tutar += (item.Menu.Fiyat * item.Quantity);
						}

                        if (masaKodu == null && siprais.Tutar < 250)
                        {
                            siprais.Tutar += 50;
						}

						_context.Add(SiparisMenu);
                    }
                }

                cart.Items = [];
                SaveCart(cart);

                _context.SaveChanges();

			}
            else
            {
				return RedirectToAction("Cart", new { GirisYap = true });
			}

            return RedirectToAction("Cart", new { SiparisAlındı = true} );

        }

    }
}
