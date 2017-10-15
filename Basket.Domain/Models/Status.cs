using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Domain.Models
{
    public enum Status
    {
        ItemAdded,
        ItemRemoved,
        ItemFound,
        ItemNotFound,
        StockUpdated,
        InsuffientStock
    }
}
