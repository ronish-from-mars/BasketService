using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messaging
{
    public class GetProductMsg
    {
        public readonly int ProductId;

        public GetProductMsg(int productId)
        {
            this.ProductId = productId;
        }
    }
}
