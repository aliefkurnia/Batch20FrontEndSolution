using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Master.Policy;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Master
{
    public partial class UpdatePolicy
    {
        private PolicyDto _policyDto = new();
        private SuccessNotification? _notification;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IPolicyHttpRepository? PolicyHttpRepository { get; set; }

        private async Task Update()
        {
            await PolicyHttpRepository.UpdatePolicy(_policyDto);
            _notification.Show("/policy");
        }

        protected async override Task OnInitializedAsync()
        {
            //fetch data product agar bisa tampil di page update
            _policyDto = await PolicyHttpRepository.GetPolicyById(Id);
            await PolicyHttpRepository.GetPolicy();
            //await ServiceTaskHttpRepository.GetServiceTaskById(Id);
        }
    }
}
