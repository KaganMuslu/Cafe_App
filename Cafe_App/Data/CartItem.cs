using Cafe_App.Models;

public class CartItem
{
    public int UrunId { get; set; }
    public int MenuId { get; set; }
    public string KullaniciId { get; set; }
    public float Fiyat { get; set; }
    public int Quantity { get; set; } = 1; // Default olarak 1 adet eklenir

    public Urun? Urun { get; set; }
    public Menu? Menu { get; set; }
}

public class Cart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}
