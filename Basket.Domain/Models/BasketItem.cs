namespace BasketService.Domain.Models
{
    public sealed class BasketItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
