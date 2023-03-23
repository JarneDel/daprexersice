namespace BasketService.Models;

public class CustomerBasket
{
    public string Email { get; set; }
    public List<BasketItem> Items { get; set; }
    public decimal TotalPrice { get; set; }
}