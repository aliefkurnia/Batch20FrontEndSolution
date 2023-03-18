using System.Text;
using Realta.Contract.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing
{
    public class VendorHttpRepository : IVendorHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public VendorHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<VendorDto>> GetVendors()
        {
            var response = await _httpClient.GetAsync("vendor");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var vendors = JsonSerializer.Deserialize<List<VendorDto>>(content, _options); //untuk inject filenya 

            return vendors;
        }

        public async Task<PagingResponse<VendorDto>> GetVendorPaging(VendorParameters vendorsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = vendorsParameters.PageNumber.ToString(),
                ["Keyword"] = vendorsParameters.Keyword == null ? "" : vendorsParameters.Keyword,
                ["orderBy"] = vendorsParameters.OrderBy
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("vendor/paging",queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<VendorDto>
            {
                Items = JsonSerializer.Deserialize<List<VendorDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task DeleteVendor(int id)
        {
            var url = Path.Combine("vendor", id.ToString());
            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task CreateVendor(VendorDto vendorCreate)
        {
            var content = JsonSerializer.Serialize(vendorCreate);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _httpClient.PostAsync("vendor", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }
    }
}
