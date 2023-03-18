using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class PriceItemsTable
    {
        [Parameter]
        public List<PriceItemsDto> priceItems { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateServiceTask/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var prit = priceItems.FirstOrDefault(p => p.PritId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Service Task {prit.PritName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);

            }
        }
    }
}
