using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components;

public partial class TableToolbar
{
    private Timer _timer;
    
    public string Keyword;
    [Parameter] public EventCallback<string> OnSearchChanged { get; set; }
    [Parameter] public string Placeholder { get; set; }
    private void SearchChanged()
    {
        if (_timer != null)
            _timer.Dispose();
        _timer = new Timer(OnTimerElapsed, null, 500, 0);
    }

    private void OnTimerElapsed(object sender)
    {
        OnSearchChanged.InvokeAsync(Keyword);
        _timer.Dispose();
    }
    
    

}