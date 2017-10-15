using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Messaging
{
    public class UpdateProductStockMsg
    {
        public readonly int ProductId;
        public readonly int Quantity;

        public UpdateProductStockMsg(int productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
