using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using System.Text.Json;

namespace Realta.Frontend.HttpRepository.Master
{
    public class ProvincesHttpRepository : IProvincesHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProvincesHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ProvincesDto>> GetProvinces()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/provinces
            var response = await _httpClient.GetAsync("provinces");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var provinces = JsonSerializer.Deserialize<List<ProvincesDto>>(content, _options);
            return provinces;
        }

        public async Task<PagingResponse<ProvincesDto>> GetProvincesPaging(ProvincesParameter provincesParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = provincesParameter.PageNumber.ToString(),
                ["searchTerm"] = provincesParameter.SearchTerm == null ? "" : provincesParameter.SearchTerm,
                ["countryId"] = provincesParameter.CountryId ?? ""

            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("provinces/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<ProvincesDto>
            {
                Items = JsonSerializer.Deserialize<List<ProvincesDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;

        }

        public async Task CreateProvinces(ProvincesCreateDto provincesCreateDto)
        {
            var content = JsonSerializer.Serialize(provincesCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("provinces", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdateProvinces(ProvincesDto provincesDto)
        {
            var content = JsonSerializer.Serialize(provincesDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("provinces", provincesDto.ProvId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<ProvincesDto> GetProvincesById(int id)
        {
            var url = Path.Combine("provinces", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var prov = JsonSerializer.Deserialize<ProvincesDto>(content, _options);
            return prov;
        }

        public async Task deleteProvinces(int id)
        {
            var url = Path.Combine("provinces", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
