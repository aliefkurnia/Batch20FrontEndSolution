using System.Data;
using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Purchasing;

public partial class ListOrder
{
    [Inject] public IPurchaseOrderHttpRepository Repo { get; set; }
    public List<PurchaseOrderDto> DataList { get; set; } = new();
    public MetaData MetaData { get; set; } = new();
    private PurchaseOrderParameters _param = new();
    private SuccessNotification _notif;
    private DeleteModal _del;

    protected async override Task OnInitializedAsync()
    {
        // DataList = await Repo.Get();
        await Get();
    }

    private async Task Get()
    {
        var response = await Repo.GetHeaders(_param);
        DataList = response.Items;
        MetaData = response.MetaData;
    }

    private StatusUpdateDto toUpdate = new();

    private async Task OnUpdate(PurchaseOrderDto data)
    {
        toUpdate.PoheNumber = data.PoheNumber;
        toUpdate.PoheStatus = data.PoheStatus;
    }

    private async Task OnUpdateConfirmed()
    {
        await Repo.UpdateStatus(toUpdate);
        _param.PageNumber = 1;
        await Get();
        _notif.Show(NavigationManager.Uri);
    }
    
    private async Task OnDelete(string id)
    {
        _del.Show(id, $"Purchase Order {id} will be deleted!");
        await Task.Delay(100);
    }

    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await Repo.DeleteHeader(id.ToString());
        _param.PageNumber = 1;
        _notif.Show("/purchasing/list-order");
        await Get();
    }
    
    public static (string, string) GetStatus(int status)
    {
        return status switch
        {
            1 => ("warning-btn", "Pending"),
            2 => ("success-btn", "Approve"),
            3 => ("close-btn", "Reject"),
            4 => ("secondary-btn", "Receive"),
            5 => ("dark-btn", "Complete")
        };
    }
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await Get();
    }
    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.Keyword = keyword;
        await Get();
    }
    // private async Task SetEntry(int entry)
    // {
    //     Console.WriteLine(@ent);
    //     _param.PageNumber = 1;
    //     _param.PageSize = entry;
    //     await Get();
    // }
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
}
