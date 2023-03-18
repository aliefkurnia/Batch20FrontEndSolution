using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;
using Microsoft.JSInterop;


namespace Realta.Frontend.Pages.Purchasing
{
    public partial class Vendor
    {
        [Inject]
        public IVendorHttpRepository VendorRepo { get; set; }
        private VendorParameters _vendorParameters = new VendorParameters();
        
        [Parameter]
        public EventCallback<int> OnDeleteConfirmed { get; set; }
        public List<VendorDto> VendorListPaging { get; set; } = new List<VendorDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        private string orderBy = ""; // menunjukkan kolom yang diurutkan
        private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)
        private VendorDto _vendor = new VendorDto();
        private SuccessNotification _notification;
        [Inject]
        public IJSRuntime JS { get; set; }
        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }
        
        protected async override Task OnInitializedAsync()
        {
            await GetVendorPaging();
        }
        private async Task GetVendorPaging()
        {
            var pagingRespon = await VendorRepo.GetVendorPaging(_vendorParameters);
            VendorListPaging = pagingRespon.Items;
            MetaData = pagingRespon.MetaData;
        }
        private async Task SelectedPage (int page)
        {
            _vendorParameters.PageNumber = page;
            await GetVendorPaging();
        }
        
        private async Task SearchChanged(string keyword)
        {
            _vendorParameters.PageNumber = 1;
            _vendorParameters.Keyword = keyword;
            await GetVendorPaging();
        }
        
        private async Task SortChanged(string columnName)
        {
            if (orderBy != columnName)
            {
                orderBy = columnName;
                sortOrder = "asc";
            }
            else
            {
                sortOrder = sortOrder == "asc" ? "desc" : "asc";
            }
            _vendorParameters.OrderBy = orderBy + " " + sortOrder;
            await GetVendorPaging();
        }
        
        private async Task Create()
        {
            await VendorRepo.CreateVendor(_vendor);
            _notification.Show("/purchasing/vendor");
            await GetVendorPaging();;
        }
        private async Task DeleteConfirmed(int id)
        {
            await VendorRepo.DeleteVendor(id);
            _vendorParameters.PageNumber = 1;
            _notification.Show("/purchasing/vendor");
            await GetVendorPaging();;
            // await OnDeleted.InvokeAsync(id);
            // await ShowSuccessModal();
        }
     

}
}
