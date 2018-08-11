namespace Bjss.PriceBasket.Core.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int QuantityAvailable { get; set; }
        public decimal Price { get; set; }
    }
}
