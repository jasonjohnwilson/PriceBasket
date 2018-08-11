using Bjss.PriceBasket.Core.Interfaces;
using System.Collections.Generic;

namespace Bjss.PriceBasket.Core.Models
{
    public class Basket : IBasket
    {
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
