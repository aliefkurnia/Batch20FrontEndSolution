using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IRegionsHttpRepository
    {
        Task<List<RegionsDto>> GetRegions();
        Task<PagingResponse<RegionsDto>> GetRegionsPaging(RegionsParameter regionsParameter);
        
        
        Task CreateRegions(RegionsCreateDto regionsCreateDto);
        Task UpdateRegions(RegionsDto regionsDto);
        Task<RegionsDto> GetRegionsById(int id);

        Task deleteRegions(int id);
    }
}
