using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class ListOrderDetail
{
    [Parameter] public string Id { get; set; } 
    [Inject] private IPurchaseOrderHttpRepository Repo { get; set; }
    private SuccessNotification _notif;
    private DeleteModal _del;
    public PurchaseOrderDto Header { get; set; } = new();
    public MetaData MetaData { get; set; } = new();

    private PurchaseOrderDetailParameters _param = new();
    public List<PurchaseOrderDetailDto> DataList { get; set; } = new();
    protected async override Task OnInitializedAsync()
    {
        Header = await Repo.GetHeader(Id);
        await Get();
    }

    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await Get();
    }
    
    private async Task Get()
    {
        var response = await Repo.GetDetails(Id, _param);
        DataList = response.Items;
        MetaData = response.MetaData;
    }
    
    private QtyUpdateDto toUpdate = new();
    private async Task OnUpdate(PurchaseOrderDetailDto data)
    {
        toUpdate.PodeId = data.PodeId;
        toUpdate.StockName = data.StockName;
        toUpdate.PodeOrderQty = data.PodeOrderQty;
        toUpdate.PodeReceivedQty = data.PodeReceivedQty;
        toUpdate.PodeRejectedQty = data.PodeRejectedQty;
    }
    
    private async Task OnUpdateConfirmed()
    {
        await Repo.UpdateQty(toUpdate);
        await Get();
        _param.PageNumber = 1;
        if (DataList.Any())
        {
            _param.PageNumber = 1;
            _notif.Show(NavigationManager.Uri);
        }
        else
        {
            _notif.Show($"/purchasing/list-order");
        }
    }

    private async Task OnDelete(int id)
    {
        _del.Show<int>(id, $"Purchase Order {id} will be deleted!");
        Console.WriteLine("ini liat" + OnDeleteConfirmed);
        await Task.Delay(100);
    }

    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await Repo.DeleteDetail((int)id);
        await Get();
        if (DataList.Any())
        {
            _param.PageNumber = 1;
            _notif.Show(NavigationManager.Uri);
        }
        else
        {
            _notif.Show($"/purchasing/list-order");
        }
    }
    
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.Keyword = keyword;
        await Get();
    }
    
    private string orderBy = ""; // menunjukkan kolom yang diurutkan
    private string sortOrder = "asc"; // menunjukkan urutan sortir (asc atau desc)
    private async Task SortChanged(string columnName)
    {
        if (orderBy != columnName)
        {
            // kolom baru yang di klik, urutan sortir diatur ulang ke ascending
            orderBy = columnName;
            sortOrder = "asc";
        }
        else
        {
            // kolom yang sama yang di klik, urutan sortir diubah antara ascending dan descending
            sortOrder = sortOrder == "asc" ? "desc" : "asc";
        }
    
        _param.OrderBy = orderBy + " " + sortOrder; // menambahkan urutan sortir baru ke parameter
        await Get();
    }
    
    public static (string, string) GetStatus(int status)
    {
        return status switch
        {
            1 => ("warning-btn", "Pending"),
            2 => ("info-btn", "Approve"),
            3 => ("danger-btn", "Reject"),
            4 => ("secondary-btn", "Receive"),
            5 => ("dark-btn", "Complete")
        };
    }
} 
