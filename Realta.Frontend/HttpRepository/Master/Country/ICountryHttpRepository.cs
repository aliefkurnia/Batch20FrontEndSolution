using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface ICountryHttpRepository
    {
        Task<List<CountryDto>> GetCountry();

        Task<PagingResponse<CountryDto>> GetCountryPaging(CountryParameters countryParameters);
        
        Task CreateCountry(CountryCreateDto countryCreateDto);
        Task UpdateCountry(CountryDto countryDto);
        Task<CountryDto> GetCountryById(int id);

        Task deleteCountry(int id);
    }
}
