using System.Text;
using Realta.Contract.Models;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master
{
    public class RegionsHttpRepository : IRegionsHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public RegionsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<RegionsDto>> GetRegions()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/regions
            var response = await _httpClient.GetAsync("regions");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var region = JsonSerializer.Deserialize<List<RegionsDto>>(content, _options);
            return region;
        }

        public async Task<PagingResponse<RegionsDto>> GetRegionsPaging(RegionsParameter regionsParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = regionsParameter.PageNumber.ToString(),
                ["searchTerm"] = regionsParameter.SearchTerm == null ? "" : regionsParameter.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("regions/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<RegionsDto>
            {
                Items = JsonSerializer.Deserialize<List<RegionsDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;

        }

        public async Task CreateRegions(RegionsCreateDto regionsCreateDto)
        {
            var content = JsonSerializer.Serialize(regionsCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("regions", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdateRegions(RegionsDto regionsDto)
        {
            var content = JsonSerializer.Serialize(regionsDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("regions", regionsDto.regionCode.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<RegionsDto> GetRegionsById(int id)
        {
            var url = Path.Combine("regions", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var regions = JsonSerializer.Deserialize<RegionsDto>(content, _options);
            return regions;
        }

        
        public async Task deleteRegions(int id)
        {
            var url = Path.Combine("regions", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
