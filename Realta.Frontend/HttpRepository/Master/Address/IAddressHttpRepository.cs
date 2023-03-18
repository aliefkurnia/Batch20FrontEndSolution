using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IAddressHttpRepository
    {
        Task<List<AddressDto>> GetAddress();

        Task<PagingResponse<AddressDto>> GetAddressPaging(AddressParameter addressParameter);
        
        Task CreateAddress(AddressCreateDto addressCreateDto);
        Task UpdateAddress(AddressDto addressDto);
        Task<AddressDto> GetAddressById(int id);

        Task deleteAddress(int id);
    }
}
