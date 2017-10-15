using Basket.Actors.Messaging;
using Basket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messages
{
    public sealed class AddItemToBasketMsg : BasketOperationMsg
    {
        public readonly Product Product;

        public readonly int Quantity;

        public AddItemToBasketMsg(int customerId, Product product, int quantity) : base(customerId)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}
