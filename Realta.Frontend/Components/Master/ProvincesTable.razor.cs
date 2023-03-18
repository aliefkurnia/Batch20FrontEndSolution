using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class ProvincesTable
    {
        [Parameter]
        public List<ProvincesDto> Provinces { get; set; }
        
        [Parameter]
        public EventCallback<int> OnSelectedProvinces { get; set; }
        
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateProvinces/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var provinces = Provinces.FirstOrDefault(p => p.ProvId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Provinces {provinces.ProvName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
                
            }
        }

        private async void SelectProvinces(int id)
        {
            await OnSelectedProvinces.InvokeAsync(id);
        }
    }
}
