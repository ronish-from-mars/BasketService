namespace BasketService.Actors.Messaging
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
