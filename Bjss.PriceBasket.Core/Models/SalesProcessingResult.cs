using System.Collections.Generic;
using System.Linq;

namespace Bjss.PriceBasket.Core.Models
{
    public class SalesProcessingResult
    {
        public bool Success { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = Enumerable.Empty<ValidationError>();
        public SaleDetails SaleDetails { get; set; }
    }
}
