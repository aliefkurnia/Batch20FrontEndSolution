using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;
using System.Threading.Tasks;

namespace Realta.Frontend.Pages.Master
{
    public partial class ServiceTask
    {
        [Inject] public IServiceTaskHttpRepository ServiceTaskRepository { get; set; }
        public List<ServiceTaskDto> ServiceTaskList { get; set; } = new List<ServiceTaskDto>();


        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ServiceTaskList = await ServiceTaskRepository.GetServiceTask();
            await GetPaging();
            await ServiceTaskHttp.GetServiceTask();

        }

        private ServiceTaskParameter _serviceTaskParameter = new ServiceTaskParameter();
        public MetaData MetaData { get; set; } = new MetaData();

        private async Task SelectedPage(int page)
        {
            _serviceTaskParameter.PageNumber = page;
            await GetPaging();
        }

        private async Task GetPaging()
        {
            var response = await ServiceTaskRepository.GetServiceTaskPaging(_serviceTaskParameter);
            ServiceTaskList = response.Items;
            MetaData = response.MetaData;
            Console.WriteLine(response.Items);

        }


        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _serviceTaskParameter.PageNumber = 1;
            _serviceTaskParameter.SearchTerm = searchTerm;
            await GetPaging();
        }



        private ServiceTaskCreateDto _serviceTaskDto = new ServiceTaskCreateDto();

        private SuccessNotification _notification;

        [Inject] public IServiceTaskHttpRepository ServiceTaskHttp { get; set; }

        private async Task Create()
        {
            await ServiceTaskHttp.CreateServiceTask(_serviceTaskDto);
            _notification.Show("/servicetask");
            _serviceTaskDto = new ServiceTaskCreateDto();
            await GetPaging();
        }

        private async Task deleteServiceTask(int id)
        {
            await ServiceTaskRepository.deleteServiceTask(id);
            _serviceTaskParameter.PageNumber = 1;
            await GetPaging();
        }

    }
}
