using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Master
{
    public partial class UpdateServiceTask
    {
        private ServiceTaskDto _serviceTaskDto = new();
        private SuccessNotification? _notification;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IServiceTaskHttpRepository? ServiceTaskHttpRepository { get; set; }

        private async Task Update()
        {
            await ServiceTaskHttpRepository.UpdateServiceTask(_serviceTaskDto);
            _notification.Show("/servicetask");
        }

        protected async override Task OnInitializedAsync()
        {
            //fetch data product agar bisa tampil di page update
            _serviceTaskDto = await ServiceTaskHttpRepository.GetServiceTaskById(Id);
            await ServiceTaskHttpRepository.GetServiceTask();
            //await ServiceTaskHttpRepository.GetServiceTaskById(Id);
        }
    }
}
