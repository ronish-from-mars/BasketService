using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messaging
{
    public abstract class BasketOperationMsg
    {
        public readonly int CustomerId;

        public BasketOperationMsg(int customerId)
        {
            this.CustomerId = customerId;
        }
    }
}
