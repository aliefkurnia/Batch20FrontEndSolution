using Realta.Contract.Models;
using Realta.Frontend.Features;
using Realta.Domain.RequestFeatures;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IStocksHttpRepository
{
    Task<List<StocksDto>> GetStocks();
    Task<PagingResponse<StocksDto>> GetStocksPaging(StocksParameters stocksParameters);
    Task<List<StockPhotoDto>> GetStocksPhoto(int stockId);
    Task<StocksDto> GetStockById(int stockId);
    Task UpdateStock(StocksDto stocksUpdateDto);
    Task CreateStock(StocksDto stockCreateDto);
    Task DeleteStock(int stockId);
}

