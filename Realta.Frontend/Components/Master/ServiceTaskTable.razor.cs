using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class ServiceTaskTable
    {
        [Parameter]
        public List<ServiceTaskDto> ServiceTask { get; set; }

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
            var servicetask = ServiceTask.FirstOrDefault(p => p.SetaId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Service Task {servicetask.SetaName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
                
            }
        }
    }
}
