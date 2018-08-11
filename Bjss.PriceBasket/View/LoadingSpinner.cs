using Bjss.PriceBasket.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.View
{
    /// <summary>
    /// Loading image displayed to the console
    /// </summary>
    public class LoadingSpinner : ILoadingSpinner
    {
        private bool  _show = false;
        private Task _task;
        private readonly IConsole _console;

        public LoadingSpinner(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Displays the processing message and starts a loading spinner representation
        /// </summary>
        public void Show()
        {
            _show = true;
            _console.WriteLine("Processing...");            
            _task = Task.Run(() => StartSpinner());
        }
        
        /// <summary>
        /// Clears the loading spinner from the console
        /// </summary>
        public void Hide()
        {
            _show = false;
            _task.Wait();
            _console.Clear();
        }

        private void StartSpinner()
        {
            int currentIndex = 0;
            char[] spinnerChars = { '|', '/', '-', '\\', '-' };
            const int charCount = 5;

            while (_show)
            {
                currentIndex++;
                currentIndex = currentIndex % charCount;
                _console.Write(spinnerChars[currentIndex]);
                _console.SetCursorPosition(_console.CursorLeft - 1, _console.CursorTop);
                Thread.Sleep(100);
            }
        }
    }
}
