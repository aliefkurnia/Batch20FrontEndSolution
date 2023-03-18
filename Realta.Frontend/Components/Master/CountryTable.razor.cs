using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class CountryTable
    {
        [Parameter]
        public List<CountryDto> Country { get; set; }
        
        [Parameter]
        public EventCallback<int> OnSelectedCountry { get; set; }
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateCountry/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var country = Country.FirstOrDefault(p => p.CountryId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Country {country.CountryId} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
        
        private async void SelectCountry(int id)
        {
            await OnSelectedCountry.InvokeAsync(id);
        }
    }
}
