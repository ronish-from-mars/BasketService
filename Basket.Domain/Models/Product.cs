namespace BasketService.Domain.Models
{
    public sealed class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string PackshotImageUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityAvailable { get; set; }
    }
}
