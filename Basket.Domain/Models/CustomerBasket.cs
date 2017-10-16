namespace BasketService.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public sealed class CustomerBasket
    {
        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public int TotalItems { get; set; }

        public decimal Subtotal { get; set; }
    }
}
