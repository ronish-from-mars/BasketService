using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Domain.Models
{
    public class CustomerBasket
    {
        public Guid Id { get; set; }

        public string CustomerId { get; set; }

        public List<BasketItem> Items { get; set; }

        public int TotalItems { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }
    }
}
