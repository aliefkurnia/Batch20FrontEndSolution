using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Components.Purchasing
{
    public partial class ModalCreateStock
    {
        private string _modalDisplay;
        private string _modalClass;
        private bool _showBackdrop;
        [Parameter] public EventCallback getPaging { get; set; }

        public void Show()
        {
            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            StateHasChanged();
        }

        private void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
        }

        public StocksDto _createStock { get; set; } = new StocksDto();

        private SuccessNotification _notification;

        [Inject]
        public IStocksHttpRepository StocksHttpRepository { get; set; }
        private async Task Create()
        {
            await StocksHttpRepository.CreateStock(_createStock);
            _notification.Show("/purchasing/stocks");
            await getPaging.InvokeAsync();
        }
    }
}
