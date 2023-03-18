using Realta.Contract.Models;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Purchasing;

public interface IPurchaseOrderHttpRepository
{
    Task<PagingResponse<PurchaseOrderDto>> GetHeaders(PurchaseOrderParameters param);
    Task<PagingResponse<PurchaseOrderDetailDto>> GetDetails(string po, PurchaseOrderDetailParameters param);
    Task<PurchaseOrderDto> GetHeader(string po);
    Task Create(PurchaseOrderTransfer data);
    Task UpdateStatus(StatusUpdateDto data);
    Task UpdateQty(QtyUpdateDto data);
    Task DeleteHeader(string id);
    Task DeleteDetail(int id);
}
