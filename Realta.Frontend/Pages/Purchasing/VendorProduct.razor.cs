using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Pages.Purchasing;

public partial class VendorProduct
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IVendorProductHttpRepository VendproRepo { get; set; }
    public List<VendorProductDto> VenproList { get; set; } = new List<VendorProductDto>();
    
    public MetaData MetaData { get; set; } = new MetaData();
    private VendorParameters _vendproParameters = new VendorParameters();
    public List<VendorProductDto> VendorListPaging { get; set; } = new List<VendorProductDto>();
    protected async override Task OnInitializedAsync()
    {
        //VenproList = await VendproRepo.GetVendorProduct(Id);
        await GetVendorPaging();
    }
    
    private async Task GetVendorPaging()
    {
        var pagingRespon = await VendproRepo.GetVenProPaging(_vendproParameters, Id);
        VendorListPaging = pagingRespon.Items;
        MetaData = pagingRespon.MetaData;
    }
    
}