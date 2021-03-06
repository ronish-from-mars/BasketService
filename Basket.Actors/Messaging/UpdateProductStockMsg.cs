﻿namespace BasketService.Actors.Messaging
{
    public sealed class UpdateProductStockMsg
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
