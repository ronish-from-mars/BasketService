namespace BasketService.Domain.Dtos
{
    public sealed class ProductDto
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
