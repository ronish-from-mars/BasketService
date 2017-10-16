namespace BasketService.Actors.Messaging
{
    public sealed class GetCustomerBasketMsg : BasketOperationMsg
    {
        public GetCustomerBasketMsg(int customerId) : base(customerId)
        {
            
        }
    }
}
