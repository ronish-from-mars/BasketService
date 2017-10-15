using Akka.Actor;
using Basket.Actors.Messages;
using Basket.Actors.Messaging;
using Basket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Actors.Baskets
{
    public sealed class BasketActor : ReceiveActor
    {
        public CustomerBasket BasketState { get; set; }

        private IActorRef ProductsActorRef { get; set; }

        public BasketActor(IActorRef productsActor)
        {
            this.BasketState = new CustomerBasket();
            this.ProductsActorRef = productsActor;

            Receive<GetCustomerBasketMsg>(_ => Sender.Tell(this.BasketState));
            ReceiveAsync<AddItemToBasketMsg>(m => AddItemToBasketActionAsync(m).PipeTo(Sender), m => m.Quantity > 0);
            Receive<RemoveItemFromBasketMsg>(m => Sender.Tell(RemoveItemToBasketAction(m)));
        }

        private async Task<CustomerBasket> AddItemToBasketActionAsync(AddItemToBasketMsg message)
        {
            var result = await this.ProductsActorRef.Ask<Domain.Models.Status>(
                new UpdateProductStockMsg(
                    productId: message.Product.ProductId,
                    quantity: -message.Quantity
                )
            );

            switch (result)
            {
                case Domain.Models.Status.StockUpdated:
                    return AddItemToBasket(message.CustomerId, message.Product, message.Quantity);
                default:
                    return AddItemToBasket(message.CustomerId, message.Product, message.Quantity); // return current basket
            }

        }

        private CustomerBasket AddItemToBasket(int customerBasketId, Product product, int quantity)
        {
            // check if product was added previously
            var existingBasketItem = this.BasketState.Items.Find(item => item.Product.ProductId == product.ProductId);

            if (existingBasketItem is BasketItem)
            {
                // calculate increase in price
                var increaseInAmount = existingBasketItem.Product.UnitPrice * quantity + this.BasketState.Total;

                // calculate increase in total items
                var totalItems = this.BasketState.TotalItems + quantity;

                this.BasketState.Total = increaseInAmount;
                this.BasketState.TotalItems = totalItems;

                return this.BasketState;
            }
            else
            {
                // create a new basket item/product
                var basketItemId = Guid.NewGuid();
                this.BasketState.Items.Add(new BasketItem
                {
                    Id = basketItemId,
                    Product = product,
                    Quantity = quantity,

                });

                return this.BasketState;
            }
        }

        public bool RemoveItemToBasketAction(RemoveItemFromBasketMsg message)
        {
            var basketItem = this.BasketState.Items.Find(item => item.Id == message.BasketItemId);

            if (basketItem is BasketItem)
            {
                this.BasketState.Items.Remove(basketItem);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketActor(productsActor));
        }
    }
}
