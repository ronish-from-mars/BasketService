namespace BasketService.Actors.Messages
{
    using BasketService.Actors.Messaging;

    public sealed class AddItemToBasketMsg : BasketOperationMsg
    {
        public readonly int ProductId;

        public readonly int Quantity;

        public AddItemToBasketMsg(int customerId, int productId, int quantity) : base(customerId)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
