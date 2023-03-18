using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master.CategoryGroup
{
    public interface ICategoryGroupHttpRepository
    {
        Task<List<CategoryGroupDto>> GetCategoryGroup();

        Task<List<PolicyDto>> GetPolicy();
        Task<PagingResponse<CategoryGroupDto>> GetCategoryGroupPaging(CategoryGroupParameter categoryGroupParameter);
        
        Task CreateCategoryGroup(CategoryGroupCreateDto categoryGroupCreateDto);
        Task UpdateCategoryGroup(CategoryGroupDto categoryGroupDto);
        Task<CategoryGroupDto> GetCategoryGroupById(int id);

        Task deleteCategoryGroup(int id);
    }
}
