using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;

namespace Realta.Frontend.Components.Purchasing
{
    public partial class ModalUploadFotoStock
    {
        [Parameter] public List<StockPhotoDto> PhotoList { get; set; }
        [Parameter] public int Id { get; set; }
    }
}
