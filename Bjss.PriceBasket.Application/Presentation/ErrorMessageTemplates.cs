namespace Bjss.PriceBasket.Console.View
{
    /// <summary>
    /// Const error messages.  Please not in a production application these would be added to a resource file.
    /// </summary>
    public class ErrorMessageTemplates
    {
        public const string NoProductsSelected = "No products have currently been added to the basket";
        public const string ProductOutOfStock = "Product {0} is currently out of stock";
        public const string ProductNotFound = "No product exists matching the named {0}";
    }
}
