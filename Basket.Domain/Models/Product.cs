using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string PackshotImageUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public int QuantityAvailable { get; set; }
    }
}
