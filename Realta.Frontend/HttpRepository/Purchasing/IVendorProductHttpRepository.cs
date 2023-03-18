using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;


namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IVendorProductHttpRepository
{
    Task<List<VendorProductDto>> GetVendorProduct( int id);
   Task<PagingResponse<VendorProductDto>> GetVenProPaging(VendorParameters vendorsParameters, int id);
}