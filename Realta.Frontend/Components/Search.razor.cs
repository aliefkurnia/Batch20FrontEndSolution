using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components
{
    public partial class Search
    {
        private Timer _timer;

        public string SearchTerm;

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(OnTimerElapsed, null, 500, 0);
        }

        private void OnTimerElapsed(object sender)
        {
            OnSearchChanged.InvokeAsync(SearchTerm);
            _timer.Dispose();
        }
    }
}
