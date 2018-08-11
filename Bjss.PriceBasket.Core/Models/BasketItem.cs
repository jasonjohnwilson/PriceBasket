using System;
using System.Collections.Generic;
using System.Text;

namespace Bjss.PriceBasket.Core.Models
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
