using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master.Policy;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Master
{
    public partial class Policy
    {
        [Inject]
        public  IPolicyHttpRepository PolicyRepository { get; set; }
        public  List<PolicyDto> PolicyList { get; set; } = new List<PolicyDto>();

        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            PolicyList = await PolicyRepository.GetPolicy();
            await GetPaging();
            await PolicyHttp.GetPolicy();
        }

        private PolicyParameter _policyParameter = new PolicyParameter();
        private MetaData MetaData { get; set; } = new MetaData();

        private async Task SelectedPage(int page)
        {
            _policyParameter.PageNumber = page;
            await GetPaging();
        }

        private async Task GetPaging()
        {
            var response = await PolicyRepository.GetPolicyPaging(_policyParameter);
            PolicyList = response.Items;
            MetaData = response.MetaData;
        }
        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _policyParameter.PageNumber = 1;
            _policyParameter.SearchTerm = searchTerm;
            await GetPaging();
        }

        private PolicyCreateDto _policyCreateDto= new PolicyCreateDto();

        private SuccessNotification _notification;

        [Inject] public IPolicyHttpRepository PolicyHttp { get; set; }

        private async Task Create()
        {
            await PolicyHttp.CreatePolicy(_policyCreateDto);
            _notification.Show("/policy");
            _policyCreateDto = new();
        }

        private async Task DeletePolicy(int id)
        {
            await PolicyRepository.DeletePolicy(id);
            _policyParameter.PageNumber = 1;
            await GetPaging();
        }
    }
}
