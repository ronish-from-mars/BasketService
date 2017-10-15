using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messaging
{
    public class GetCustomerBasketMsg : BasketOperationMsg
    {
        public GetCustomerBasketMsg(int customerId) : base(customerId)
        {
            
        }
    }
}
