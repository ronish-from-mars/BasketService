using Basket.Actors.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messages
{
    public sealed class RemoveItemFromBasketMsg : BasketOperationMsg
    {
        public readonly Guid BasketItemId;

        public RemoveItemFromBasketMsg(int customerId, Guid basketItemId) : base(customerId)
        {
            this.BasketItemId = basketItemId;
        }
    }
    
}
