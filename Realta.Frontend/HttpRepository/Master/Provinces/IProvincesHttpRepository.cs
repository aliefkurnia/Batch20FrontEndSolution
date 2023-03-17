﻿using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Master
{
    public interface IProvincesHttpRepository
    {
        Task<List<ProvincesDto>> GetProvinces();

        Task<PagingResponse<ProvincesDto>> GetProvincesPaging(ProvincesParameter provincesParameter);
    }
}
