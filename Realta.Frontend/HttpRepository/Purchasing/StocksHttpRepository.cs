using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public class StocksHttpRepository : IStocksHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public StocksHttpRepository( HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options =  new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task CreateStock(StocksDto stockCreateDto)
    {
        var content = JsonSerializer.Serialize(stockCreateDto);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var postResult = await _httpClient.PostAsync("stocks", bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }

    public async Task DeleteStock(int stockId)
    {
        var url = Path.Combine("stocks", stockId.ToString());

        var deleteResult = await _httpClient.DeleteAsync(url);
        var deleteContent = await deleteResult.Content.ReadAsStringAsync();

        if (!deleteResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(deleteContent);
        }
    }

    public async Task<StocksDto> GetStockById(int stockId)
    {
        string url = Path.Combine("stocks", stockId.ToString());
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stocks = JsonSerializer.Deserialize<StocksDto>(content, _options);
        return stocks;
    }

    public async Task<List<StocksDto>> GetStocks()
    {
        // call api end point e.g : http://localhost:7068/api/stocks
        var response = await _httpClient.GetAsync("stocks");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stocks = JsonSerializer.Deserialize<List<StocksDto>>(content, _options);
        return stocks;
    }

    public async Task<PagingResponse<StocksDto>> GetStocksPaging(StocksParameters stocksParameters)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = stocksParameters.PageNumber.ToString(),
            ["searchTerm"] = stocksParameters.SearchTerm == null ? "" : stocksParameters.SearchTerm.ToString(),
            ["orderBy"] = stocksParameters.OrderBy,
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("stocks/pageList", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<StocksDto>
        {
            Items = JsonSerializer.Deserialize<List<StocksDto>>(content, _options),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
        };

        return pagingResponse;
    }

    public async Task<List<StockPhotoDto>> GetStocksPhoto(int stockId)
    {
        // call api end point e.g : http://localhost:7068/api/stock_photo/{id}
        string url = Path.Combine("stock_photo", stockId.ToString());
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var stockPhoto = JsonSerializer.Deserialize<List<StockPhotoDto>>(content, _options);
        return stockPhoto;
    }

    public async Task UpdateStock(StocksDto stocksUpdateDto)
    {
        var content = JsonSerializer.Serialize(stocksUpdateDto);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var url = Path.Combine("stocks", stocksUpdateDto.StockId.ToString());

        var postResult = await _httpClient.PutAsync(url, bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }
}


