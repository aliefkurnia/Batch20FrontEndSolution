using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using System.Diagnostics.Contracts;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master.CategoryGroup;
using Realta.Frontend.HttpRepository.Master.ServiceTask;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Master
{
    public partial class CategoryGroup 
     {
            [Inject]
            public ICategoryGroupHttpRepository CategoryGroupRepository { get; set; }
            public List<CategoryGroupDto> CategoryGroupList { get; set; } = new List<CategoryGroupDto>();

            private List<PolicyDto> policyDto = new List<PolicyDto>();
            public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //CategoryGroupList = await CategoryGroupRepository.GetCategoryGroup();
            policyDto = await CategoryGroupRepository.GetPolicy();
            await GetPaging();
            await CategoryGroupHttp.GetCategoryGroup();
        }

        private CategoryGroupParameter _categoryGroupParameter = new CategoryGroupParameter();
        private MetaData MetaData { get; set; } = new MetaData();

        private async Task SelectedPage(int page)
        {
            _categoryGroupParameter.PageNumber = page;
            await GetPaging();
        }

        private async Task GetPaging()
        {
            var response = await CategoryGroupRepository.GetCategoryGroupPaging(_categoryGroupParameter);
            CategoryGroupList = response.Items;
            MetaData = response.MetaData;
        }

        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _categoryGroupParameter.PageNumber = 1;
            _categoryGroupParameter.SearchTerm = searchTerm;
            await GetPaging();
        }
        
        private CategoryGroupCreateDto _categoryGroupCreateDto = new CategoryGroupCreateDto();
        private SuccessNotification _notification;

        [Inject] public ICategoryGroupHttpRepository CategoryGroupHttp { get; set; }

        private async Task Create()
        {
            await CategoryGroupHttp.CreateCategoryGroup(_categoryGroupCreateDto);
            _notification.Show("/categorygroup");
            _categoryGroupCreateDto = new();
        }

        private async Task deleteCategoryGroup(int id)
        {
            await CategoryGroupRepository.deleteCategoryGroup(id);
            _categoryGroupParameter.PageNumber = 1;
            await GetPaging();
        }
        
    }
}
