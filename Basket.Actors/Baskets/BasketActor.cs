namespace BasketService.Actors.Baskets
{
    using System;
    using System.Threading.Tasks;

    using Akka.Actor;

    using BasketService.Actors.Messages;
    using BasketService.Actors.Messaging;
    using BasketService.Domain.Models;

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
            ReceiveAsync<UpdateItemQuantityMsg>(m => UpdateItemQuantityToBasketActionAsync(m).PipeTo(Sender), m => m.Quantity > 0);
            Receive<RemoveItemFromBasketMsg>(m => Sender.Tell(RemoveItemToBasketActionAsync(m)));
            Receive<ClearCustomerBasketMsg>(m => Sender.Tell(ClearBasketActionAsync(m)));
        }

        private async Task<CustomerBasket> AddItemToBasketActionAsync(AddItemToBasketMsg message)
        {
            var result = await this.ProductsActorRef.Ask<Domain.Models.Status>(
                new UpdateProductStockMsg(
                    productId: message.ProductId,
                    quantity: message.Quantity
                )
            );

            switch (result)
            {
                case Domain.Models.Status.StockUpdated:
                    return await AddItemToBasketAsync(message.CustomerId, message.ProductId, message.Quantity);
                default: // insufficient stock
                    return this.BasketState; // return current basket
            }

        }

        private async Task<CustomerBasket> AddItemToBasketAsync(int customerId, int productId, int quantity)
        {
           
            // check if product was added previously
            var existingProduct = this.BasketState.Items?.Find(item => item.Product.ProductId == productId);

            if (existingProduct != null && existingProduct is BasketItem)
            {
                // calculate increase in price
                var increaseInAmount = this.BasketState.Subtotal + (existingProduct.Product.UnitPrice * quantity);

                existingProduct.Quantity = existingProduct.Quantity + quantity;

                // calculate increase in total items
                var totalItems = this.BasketState.TotalItems + quantity;

                this.BasketState.Subtotal = increaseInAmount;
                this.BasketState.TotalItems = totalItems;

                return this.BasketState;
            }
            else
            {

                var product = await this.ProductsActorRef.Ask<Product>(new GetProductMsg(productId));

                if (product != null && product is Product)
                {
                    // reinit basket total items and total price
                    this.BasketState.TotalItems = this.BasketState.TotalItems + quantity;
                    this.BasketState.Id = Guid.NewGuid();
                    this.BasketState.CustomerId = customerId;
                    this.BasketState.Subtotal = this.BasketState.Subtotal + (quantity * product.UnitPrice);

                    // create a new basket item/product
                    this.BasketState.Items.Add(new BasketItem
                    {
                        Product = product,
                        Quantity = quantity,
                    });
                }

                return this.BasketState;
            }
        }

        private async Task<CustomerBasket> UpdateItemQuantityToBasketActionAsync(UpdateItemQuantityMsg message)
        {
            // check if product was added previously
            var existingProduct = this.BasketState.Items?.Find(item => item.Product.ProductId == message.ProductId);

            if (existingProduct != null && existingProduct is BasketItem)
            {
                var previousQuantity = existingProduct.Quantity;

                var newQuantity = message.Quantity;

                var quantityDiff = newQuantity - previousQuantity;

                // update product stock
                var result = await this.ProductsActorRef.Ask<Domain.Models.Status>(
                    new UpdateProductStockMsg(
                        productId: message.ProductId,
                        quantity: quantityDiff
                    )
                );

                switch (result)
                {
                    case Domain.Models.Status.StockUpdated:
                        // calculate new price
                        var newAmount = this.BasketState.Subtotal + (existingProduct.Product.UnitPrice * quantityDiff);

                        // calculate new quantity
                        existingProduct.Quantity = existingProduct.Quantity + quantityDiff;

                        // calculate increase in total items
                        var totalItems = this.BasketState.TotalItems + quantityDiff;

                        this.BasketState.Subtotal = newAmount;
                        this.BasketState.TotalItems = totalItems;

                        return this.BasketState;

                    default: // insufficient stock
                        return this.BasketState;
                }
            }

            return this.BasketState;
        }

        public async Task<bool> RemoveItemToBasketActionAsync(RemoveItemFromBasketMsg message)
        {
            var basketItem = this.BasketState.Items?.Find(item => item.Product.ProductId == message.ProductId);

            if (basketItem != null && basketItem is BasketItem)
            {
                var itemRemoved = this.BasketState.Items.Remove(basketItem);
                if (itemRemoved)
                {
                    // reinit basket total items and total price
                    this.BasketState.TotalItems = this.BasketState.TotalItems - basketItem.Quantity;
                    this.BasketState.Subtotal = this.BasketState.Subtotal - (basketItem.Quantity * basketItem.Product.UnitPrice);

                    // update stock
                    var result = await this.ProductsActorRef.Ask<Domain.Models.Status>(
                       new UpdateProductStockMsg(
                           productId: message.ProductId,
                           quantity: -basketItem.Quantity
                       )
                     );

                    switch (result)
                    {
                        case Domain.Models.Status.StockUpdated:
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<CustomerBasket> ClearBasketActionAsync(ClearCustomerBasketMsg message)
        {
            if(this.BasketState.CustomerId == message.CustomerId)
            {
                if (this.BasketState.Items?.Count >= 0)
                {
                    foreach(var item in this.BasketState.Items)
                    {
                        // update stock for each item
                        var result = await this.ProductsActorRef.Ask<Domain.Models.Status>(
                           new UpdateProductStockMsg(
                               productId: item.Product.ProductId,
                               quantity: -item.Quantity
                           )
                         );
                    }
                }
               
                // clear items
                this.BasketState.Items?.Clear();

                // reinit basket total items and total price
                this.BasketState.TotalItems = 0;
                this.BasketState.Subtotal = 0;

            }

            // return empty basket
            return this.BasketState;
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketActor(productsActor));
        }
    }
}
