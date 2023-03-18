using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master.PriceItems;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;
using Realta.Frontend.Components.Master;

namespace Realta.Frontend.Pages.Master
{
    public partial class PriceItems
    {
        [Inject]
        public IPriceItemsHttpRepository PriceItemsRepository { get; set;}
        public  List<PriceItemsDto> PriceItemsList { get; set;} = new List<PriceItemsDto>();

        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            PriceItemsList = await PriceItemsRepository.GetPriceItems();
            await GetPaging();
            await PriceItemsHttp.GetPriceItems();

        }

        private async Task Sort(string orderBy)
        {
            _priceItemsParameters.OrderBy = orderBy;
            await GetPaging();
        }

        private PriceItemsParameters _priceItemsParameters = new PriceItemsParameters();
        public MetaData MetaData { get; set; } = new MetaData();

        private async Task SelectedPage(int page)
        {
            _priceItemsParameters.PageNumber = page;
            await GetPaging();
        }

        private async Task GetPaging()
        {
            var response = await PriceItemsRepository.GetPriceItemsPaging(_priceItemsParameters);
            PriceItemsList = response.Items;
            MetaData = response.MetaData;
            Console.WriteLine(response.Items);
        }

        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _priceItemsParameters.PageNumber = 1;
            _priceItemsParameters.SearchTerm = searchTerm;
            await GetPaging();
        }


        private PriceItemsCreateDto _priceItemsCreateDto = new PriceItemsCreateDto();

        private SuccessNotification _notification;

        [Inject] public IPriceItemsHttpRepository PriceItemsHttp { get; set; }

        private async Task Create()
        {
            await PriceItemsHttp.CreatePriceItems(_priceItemsCreateDto);
            _notification.Show("/priceitems");
            _priceItemsCreateDto = new PriceItemsCreateDto();
            await GetPaging();
        }

        private async Task deletePriceItems(int id)
        {
            await PriceItemsRepository.deletePriceItems(id);
            _priceItemsParameters.PageNumber = 1;
            await GetPaging();
            _notification.Show("/priceitems");

        }

    }
}
