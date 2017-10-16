namespace BasketService.Actors.Messaging
{
    public sealed class GetProductMsg
    {
        public readonly int ProductId;

        public GetProductMsg(int productId)
        {
            this.ProductId = productId;
        }
    }
}
