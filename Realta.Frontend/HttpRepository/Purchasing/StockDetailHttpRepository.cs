using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class StockDetailHttpRepository : IStockDetailHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public StockDetailHttpRepository( HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<StockDetailDto>> GetStockDetail(int id)
    {
        // call api end point e.g : http://localhost:7068/api/stock/{id}
        var url = Path.Combine("stock", id.ToString());
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stockDetail = JsonSerializer.Deserialize<List<StockDetailDto>>(content, _options);
        return stockDetail;
    }

    public async Task<PagingResponse<StockDetailDto>> GetStockDetailPaging(StockDetailParameters stocksParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["stockId"] = stocksParameters.StockId.ToString(),
            ["pageNumber"] = stocksParameters.PageNumber.ToString()
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("stock/pageList", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<StockDetailDto>
        {
            Items = JsonSerializer.Deserialize<List<StockDetailDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

}


