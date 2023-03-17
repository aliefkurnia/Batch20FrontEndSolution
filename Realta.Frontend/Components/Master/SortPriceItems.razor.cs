using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components.Master;

public partial class SortPriceItems
{
    [Parameter] public EventCallback<string> SortChanged { get; set; }
    
    private async Task UseSort(ChangeEventArgs eventArgs)
    {
        if (eventArgs.Value.ToString() == "-1")
            return;
        await SortChanged.InvokeAsync(eventArgs.Value.ToString());
    }
}