namespace BasketService.Actors.Products
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Akka.Actor;

    using BasketService.Actors.Messaging;
    using BasketService.Domain.Models;

    public partial class ProductsActor : ReceiveActor
    {
        private IList<Product> Products { get; set; }

        public ProductsActor(IList<Product> products)
        {
            this.Products = products;

            // receive get products catalog message, return all products
            Receive<GetProductsCatalogMsg>(_ => Sender.Tell(new ReadOnlyCollection<Product>(this.Products)));

            // receive get a product catalog message, return required product
            Receive<GetProductMsg>(m => Sender.Tell(GetProductById(m)));

            // receive update stock message, trigger action UpdateStockAction
            Receive<UpdateProductStockMsg>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public Domain.Models.Status UpdateStockAction(UpdateProductStockMsg message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.ProductId == message.ProductId);

            if (product is Product && product != null)
            {
                if (product.QuantityAvailable > 0 && message.Quantity > 0)
                {
                    product.QuantityAvailable -= message.Quantity;
                    return Domain.Models.Status.StockUpdated;
                }
                else
                {
                    return Domain.Models.Status.InsuffientStock;
                }
            }

            return Domain.Models.Status.ItemNotFound;
        }

        public Product GetProductById(GetProductMsg message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.ProductId == message.ProductId);

            if (product != null && product is Product)
            {
                return product;
            }

            return null;
        }
    }
}
