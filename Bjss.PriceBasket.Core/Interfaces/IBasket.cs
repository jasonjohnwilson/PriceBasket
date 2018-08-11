using Bjss.PriceBasket.Core.Models;
using System.Collections.Generic;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IBasket
    {
        IEnumerable<BasketItem> Items { get; set; }
    }
}
