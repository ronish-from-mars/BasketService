namespace BasketService.Actors.Messaging
{
    public sealed class ClearCustomerBasketMsg : BasketOperationMsg
    {
        public ClearCustomerBasketMsg(int customerId) : base(customerId)
        {

        }
    }
}
