using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using System.ComponentModel;

namespace Realta.Frontend.Pages.Purchasing
{
    public partial class StockDetail
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IStockDetailHttpRepository StockDetailHttpRepository { get; set;}
        public MetaData MetaData { get; set; } = new MetaData();
        public List<StockDetailDto> stocksDetailList { get; set; }

        public StockDetailParameters _stockDetailParameters = new StockDetailParameters();

        protected async override Task OnInitializedAsync()
        {
           
            await GetPaging();
        }

        private async Task SelectedPage(int page)
        {
            _stockDetailParameters.PageNumber = page;
            await GetPaging();
        }
        private async Task GetPaging()
        {
            _stockDetailParameters.StockId = Id;

            var response = await StockDetailHttpRepository.GetStockDetailPaging(_stockDetailParameters);
            stocksDetailList = response.Items;
            MetaData = response.MetaData;
        }

        public static (string, string) GetStatus(int status)
        {
            return status switch
            {
                1 => ("warning-btn", "Stocked"),
                2 => ("info-btn", "Used"),
                3 => ("danger-btn", "Broken"),
                4 => ("dark-btn", "Complete")
            };
        }
    }

}
