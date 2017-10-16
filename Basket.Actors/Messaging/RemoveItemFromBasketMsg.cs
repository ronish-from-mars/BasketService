namespace BasketService.Actors.Messages
{
    using BasketService.Actors.Messaging;

    public sealed class RemoveItemFromBasketMsg : BasketOperationMsg
    {
        public readonly int ProductId;

        public RemoveItemFromBasketMsg(int customerId, int productId) : base(customerId)
        {
            this.ProductId = productId;
        }
    }
    
}
