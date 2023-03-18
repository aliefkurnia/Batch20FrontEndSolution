using System.Text;
using Realta.Contract.Models;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master
{
        public class AddressHttpRepository : IAddressHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public AddressHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<List<AddressDto>> GetAddress()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/address
            var response = await _httpClient.GetAsync("address");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var address = JsonSerializer.Deserialize<List<AddressDto>>(content, _options);
            return address;
        }

        public async Task<PagingResponse<AddressDto>> GetAddressPaging(AddressParameter addressParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = addressParameter.PageNumber.ToString(),
                ["searchTerm"] = addressParameter.SearchTerm == null ? "" :addressParameter.SearchTerm,
                ["provId"] = addressParameter.ProvId ?? ""
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("address/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<AddressDto>
            {
                Items = JsonSerializer.Deserialize<List<AddressDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }

        public async Task CreateAddress(AddressCreateDto addressCreateDto)
        {
            var content = JsonSerializer.Serialize(addressCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("address", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task UpdateAddress(AddressDto addressDto)
        {
            var content = JsonSerializer.Serialize(addressDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("servicetask", addressDto.AddrId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<AddressDto> GetAddressById(int id)
        {
            var url = Path.Combine("address", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var address = JsonSerializer.Deserialize<AddressDto>(content, _options);
            return address;
        }

        public async Task deleteAddress(int id)
        {
            var url = Path.Combine("address", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
