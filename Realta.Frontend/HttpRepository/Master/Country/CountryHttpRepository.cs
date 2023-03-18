using System.Text;
using Realta.Contract.Models;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master
{
    public class CountryHttpRepository : ICountryHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CountryHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CountryDto>> GetCountry()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/country
            var response = await _httpClient.GetAsync("country");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var country = JsonSerializer.Deserialize<List<CountryDto>>(content, _options);
            return country;
        }

        public async Task<PagingResponse<CountryDto>> GetCountryPaging(CountryParameters countryParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = countryParameters.PageNumber.ToString(),
                ["searchTerm"] = countryParameters.SearchTerm ?? "",
                ["regionId"] = countryParameters.RegionId ?? ""
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("country/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<CountryDto>
            {
                Items = JsonSerializer.Deserialize<List<CountryDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;

        }

        public async Task CreateCountry(CountryCreateDto countryCreateDto)
        {
            var content = JsonSerializer.Serialize(countryCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("country", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdateCountry(CountryDto countryDto)
        {
            var content = JsonSerializer.Serialize(countryDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("country", countryDto.CountryId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<CountryDto> GetCountryById(int id)
        {
            var url = Path.Combine("country", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var country = JsonSerializer.Deserialize<CountryDto>(content, _options);
            return country;
        }

        public async Task deleteCountry(int id)
        {
            var url = Path.Combine("country", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
