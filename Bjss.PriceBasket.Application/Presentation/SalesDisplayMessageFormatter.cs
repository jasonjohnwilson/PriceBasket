using Bjss.PriceBasket.Application.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Linq;
using System.Text;

namespace Bjss.PriceBasket.Console.View
{
    /// <summary>
    /// Formats the results of processing the basket
    /// </summary>
    public class SalesDisplayMessageFormatter : ISalesResultMessageFormatter
    {
        /// <summary>
        /// Format the sales detail in a string format used to display to the UI
        /// </summary>
        /// <param name="saleDetails">The sale details produced from the price basket products</param>
        /// <returns>A string representation of the sale</returns>
        public string Format(SaleDetails saleDetails)
        {
            if(saleDetails == null)
            {
                throw new ArgumentNullException(nameof(saleDetails));
            }

            var messageBuilder = new StringBuilder();

            messageBuilder.Append(FormatSubTotal(saleDetails.SubTotal) + Environment.NewLine);

            FormatDiscounts(saleDetails, messageBuilder);

            messageBuilder.Append(FormatTotal(saleDetails.Total) + Environment.NewLine);

            return messageBuilder.ToString();
        }

        private void FormatDiscounts(SaleDetails saleDetails, StringBuilder messageBuilder)
        {
            foreach (var discount in saleDetails.Discounts)
            {
                messageBuilder.Append(FormatMessage(discount.Description, discount.DiscountAmount) + Environment.NewLine);
            }

            if (saleDetails.Discounts.Any() == false)
            {
                messageBuilder.Append(SalesDisplayTemplates.NoDeductions + Environment.NewLine);
            }
        }

        private string FormatTotal(decimal total)
        {
            return string.Format(SalesDisplayTemplates.TotalMessage, total.ToString("C"));
        }

        private string FormatSubTotal(decimal subTotal)
        {
            return string.Format(SalesDisplayTemplates.SubTotalMessage, subTotal.ToString("C"));
        }

        private string FormatMessage(string description, decimal discountAmount)
        {
            return $"{description}: -{discountAmount.ToString("C")}";
        }
    }
}
