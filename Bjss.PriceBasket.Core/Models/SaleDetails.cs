using System.Collections.Generic;

namespace Bjss.PriceBasket.Core.Models
{
    public class SaleDetails
    {
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<Discount> Discounts { get; set; }
    }
}