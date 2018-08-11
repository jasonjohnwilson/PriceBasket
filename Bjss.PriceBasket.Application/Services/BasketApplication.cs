using Bjss.PriceBasket.Application.Interfaces;
using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Application.Services
{
    /// <summary>
    /// Entry point for the price basket application
    /// </summary>
    public class BasketApplication : IBasketApplication
    {
        private readonly ISalesProcessingService _salesProcessingService;
        private readonly ISalesResultMessageFormatter _salesResultMessageFormatter;
        private readonly IValidationErrorsMessageFormatter _validationErrorsMessageFormatter;
        private readonly IView _view;
        
        public BasketApplication(
            ISalesProcessingService salesProcessingService,
            ISalesResultMessageFormatter salesResultMessageFormatter,
            IValidationErrorsMessageFormatter validationErrorsMessageFormatter,
            IView view)
        {
            _salesProcessingService = salesProcessingService;
            _salesResultMessageFormatter = salesResultMessageFormatter;
            _validationErrorsMessageFormatter = validationErrorsMessageFormatter;
            _view = view;
        }

        /// <summary>
        /// Executes the products passed on the command line and determimes SubTotal, Total and discounts
        /// These are displayed to the UI indirectly via a View abstraction.
        /// </summary>
        /// <param name="args">Array of products</param>
        /// <returns>Task</returns>
        public async Task ExecuteAsync(string []args)
        {
            Task<SalesProcessingResult> salesProcessingResultTask = _salesProcessingService.TryCreateSaleAsync(args);

            _view.ShowLoading();

            var salesProcessingResult = await salesProcessingResultTask;

            _view.HideLoading();

            if (salesProcessingResult.ValidationErrors.Any())
            {
                _view.Present(_validationErrorsMessageFormatter.Format(salesProcessingResult.ValidationErrors));
            }
            else
            {
                _view.Present(_salesResultMessageFormatter.Format(salesProcessingResult.SaleDetails));
            }

            _view.Present("Prss any key to exit.....");
        }
    }
}
