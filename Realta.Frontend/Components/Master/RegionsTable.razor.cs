using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class RegionsTable
    {
        [Parameter]
        public List<RegionsDto> Regions { get; set; }
        
        public List<RegionsDto> RegionsDto { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }
        
        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateRegions/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var regions = Regions.FirstOrDefault(p => p.regionCode.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Regions {regions.RegionName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
                
            }
        }
        
        [Parameter]
        public EventCallback<int> OnSelectedRegion { get; set; }

        private async void SelectRegion(int id)
        {
            await OnSelectedRegion.InvokeAsync(id);
        }
    }
}
