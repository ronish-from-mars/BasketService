namespace BasketService.Domain.Data
{
    using System.Collections.Generic;

    using BasketService.Domain.Models;

    public class Products
    {
        // harcoded products values
        public static List<Product> GetProducts()
        {
            return new List<Product> {
                new Product {
                   ProductId = 1,
                   ProductName = "Iphone X",
                   UnitPrice = 50000,
                   QuantityAvailable = 10000
                },
               new Product {
                   ProductId = 2,
                   ProductName = "Iphone 8",
                   UnitPrice = 40000,
                   QuantityAvailable = 5000
                },
               new Product {
                   ProductId = 3,
                   ProductName = "Iphone 7",
                   UnitPrice = 35000,
                   QuantityAvailable = 2500
                },
                new Product {
                   ProductId = 4,
                   ProductName = "Iphone 6S",
                   UnitPrice = 30000,
                   QuantityAvailable = 1200
                },
                new Product {
                   ProductId = 5,
                   ProductName = "Iphone 6",
                   UnitPrice = 15000,
                   QuantityAvailable = 5
                },
                new Product {
                   ProductId = 6,
                   ProductName = "Iphone 5s",
                   UnitPrice = 12000,
                   QuantityAvailable = 0
                },
                new Product {
                   ProductId = 7,
                   ProductName = "Google Pixel 2",
                   UnitPrice = 42000,
                   QuantityAvailable = 7000
                },
                new Product {
                   ProductId = 8,
                   ProductName = "Google Pixel",
                   UnitPrice = 22000,
                   QuantityAvailable = 10
                },
            };
        }
    }
}
