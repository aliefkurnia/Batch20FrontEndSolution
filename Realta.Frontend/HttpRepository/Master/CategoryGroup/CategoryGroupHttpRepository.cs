using Realta.Contract.Models;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace Realta.Frontend.HttpRepository.Master.CategoryGroup
{
    public class CategoryGroupHttpRepository : ICategoryGroupHttpRepository
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public CategoryGroupHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CategoryGroupDto>> GetCategoryGroup()
        {

            //AsyncCallback api end point e.g https://localhost:7068/api/categorygroup
            var response = await _httpClient.GetAsync("categorygroup");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);    
            }

            var cagro = JsonSerializer.Deserialize<List<CategoryGroupDto>>(content, _options);
            return cagro;
        }

        public async Task<List<PolicyDto>> GetPolicy()
        {
            var response = await _httpClient.GetAsync("policy");
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var policy = JsonSerializer.Deserialize <List<PolicyDto>>(content, _options);
            return policy;
        }

        public async Task<PagingResponse<CategoryGroupDto>> GetCategoryGroupPaging(CategoryGroupParameter categoryGroupParameter)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = categoryGroupParameter.PageNumber.ToString(),
                ["searchTerm"] = categoryGroupParameter.SearchTerm == null ? "" :categoryGroupParameter.SearchTerm
            };
            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("categorygroup/pageList", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingRespone = new PagingResponse<CategoryGroupDto>
            {
                Items = JsonSerializer.Deserialize<List<CategoryGroupDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            Console.WriteLine(pagingRespone.Items);
            return pagingRespone;
        }

        public async Task CreateCategoryGroup(CategoryGroupCreateDto categoryGroupCreateDto)
        {
            var content = JsonSerializer.Serialize(categoryGroupCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("categorygroup", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }        
        }

        public async Task UpdateCategoryGroup(CategoryGroupDto categoryGroupDto)
        {
            var content = JsonSerializer.Serialize(categoryGroupDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine("servicetask", categoryGroupDto.CagroId.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();


            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<CategoryGroupDto> GetCategoryGroupById(int id)
        {
            var url = Path.Combine("categorygroup", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var cagro = JsonSerializer.Deserialize<CategoryGroupDto>(content, _options);
            return cagro;
        }

        public async Task deleteCategoryGroup(int id)
        {
            var url = Path.Combine("categorygroup", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
