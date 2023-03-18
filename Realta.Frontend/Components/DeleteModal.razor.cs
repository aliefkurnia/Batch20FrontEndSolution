using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components;

public partial class DeleteModal
{
    [Parameter] public EventCallback<object> OnDeletedData { get; set; }
    private object _id;
    private string _state;
    private string _display;
    private string _message = "Data will be deleted!";

    public void Show<T>(T id, string msg)
    {
        _message = msg;
        _id = id;
        _state = "show";        
        _display = "block;";
    }
  
    public void Hide()
    {
        _state = "";
        _display = "none;";

    }
  
    public async Task OnDeleteConfirmed()
    {
        await OnDeletedData.InvokeAsync(_id);
        Hide();
    } 
    
}