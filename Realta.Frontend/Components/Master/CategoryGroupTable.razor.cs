using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Realta.Contract.Models;

namespace Realta.Frontend.Components.Master
{
    public partial class CategoryGroupTable
    {
        [Parameter]
        public List<CategoryGroupDto> CategoryGroups { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateCategoryGroup/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var categorygroup = CategoryGroups.FirstOrDefault(p => p.CagroId.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete Category Group {categorygroup.CagroName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
                
            }
        }
    }
}
