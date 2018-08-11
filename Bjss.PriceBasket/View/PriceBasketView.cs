using Bjss.PriceBasket.Application.Interfaces;
using Bjss.PriceBasket.Interfaces;

namespace Bjss.PriceBasket.View
{
    /// <summary>
    /// Represents the view that is used to display to the user.
    /// </summary>
    public class PriceBasketView : IView
    {
        private readonly IConsole _console;
        private readonly ILoadingSpinner _loadingSpinner;

        public PriceBasketView(
            IConsole console,
            ILoadingSpinner loadingSpinner)
        {
            _console = console;
            _loadingSpinner = loadingSpinner;
        }

        public void ShowLoading()
        {
            _loadingSpinner.Show();
        }

        public void HideLoading()
        {
            _loadingSpinner.Hide();
        }

        public void Present(string message)
        {
            _console.WriteLine(message);
        }
    }
}
