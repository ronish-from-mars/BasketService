namespace BasketService.Domain.Models
{
    public enum Status
    {
        ItemAdded,
        ItemRemoved,
        ItemFound,
        ItemNotFound,
        StockUpdated,
        InsuffientStock,
        QuantityUpdated
    }
}
