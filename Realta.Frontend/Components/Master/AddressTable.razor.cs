using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class AddressTable
    {
        [Parameter]
        public List<AddressDto> Address { get; set; }
        
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateAddress/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var address = Address.FirstOrDefault(p => p.AddrId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete addres  {address.AddrLine1} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);

            }
        }
    }
}
