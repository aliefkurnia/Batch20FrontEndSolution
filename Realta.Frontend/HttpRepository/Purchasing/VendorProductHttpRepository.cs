using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class VendorProductHttpRepository : IVendorProductHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public VendorProductHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async  Task<List<VendorProductDto>> GetVendorProduct(int id)
    {
        var response = await _httpClient.GetAsync($"vendorproduct/{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var venpro = JsonSerializer.Deserialize<List<VendorProductDto>>(content, _options); //untuk inject filenya 

        return venpro;
    }
    
    public async Task<PagingResponse<VendorProductDto>> GetVenProPaging(VendorParameters vendorsParameters, int id)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = vendorsParameters.PageNumber.ToString(),
            ["Keyword"] = vendorsParameters.Keyword == null ? "" : vendorsParameters.Keyword,
           // ["orderBy"] = vendorsParameters.OrderBy
        };


        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"vendorproduct/{id}",queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<VendorProductDto>
        {
            Items = JsonSerializer.Deserialize<List<VendorProductDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };
        return pagingResponse;
    }
}