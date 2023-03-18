using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Master;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Master
{
    public partial class Locations
    {
        [Inject] public IRegionsHttpRepository RegionsRepository { get; set; }
        [Inject] public ICountryHttpRepository CountryRepository { get; set; }
        [Inject] public IProvincesHttpRepository ProvincesRepository { get; set; }
        [Inject] public IAddressHttpRepository AddressRepository { get; set; }

        public List<RegionsDto> RegionsList { get; set; } = new();
        public List<CountryDto> CountryList { get; set; } = new();
        public List<ProvincesDto> ProvincesList { get; set; } = new();
        public List<AddressDto> AddressList { get; set; } = new();

        private ProvincesParameter _provincesParameter = new();
        private RegionsParameter _regionsParameter = new();
        private CountryParameters _countryParameters = new();
        private AddressParameter _addressParameter = new();

        public MetaData MetaDataR { get; set; } = new();
        public MetaData MetaDataC { get; set; } = new();
        public MetaData MetaDataP { get; set; } = new();
        public MetaData MetaDataA { get; set; } = new();

        public int Id { get; set; }
        private RegionsCreateDto _regionsCreateDto = new();
        private CountryCreateDto _countryCreateDto = new();
        private ProvincesCreateDto _provincesCreateDto = new();
        private AddressCreateDto _addressCreateDto = new();

        private SuccessNotification _notification;

        [Inject] public IRegionsHttpRepository RegionsHttp { get; set; }
        [Inject] public ICountryHttpRepository CountryHttp { get; set; }
        [Inject] public IProvincesHttpRepository ProvincesHttp { get; set; }
        [Inject] public IAddressHttpRepository AddressHttp { get; set; }

        private async Task SelectedPageRegions(int page)
        {
            _regionsParameter.PageNumber = page;
            await GetRegions();
        }

        private async Task SelectedPageCountry(int page)
        {
            _countryParameters.PageNumber = page;
            await GetCountry();
        }

        private async Task SelectedPageProvinces(int page)
        {
            _provincesParameter.PageNumber = page;
            await GetProvinces();
        }

        private async Task SelectedPageAddress(int page)
        {
            _addressParameter.PageNumber = page;
            await GetAddress();
        }


        protected async override Task OnInitializedAsync()
        {
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();
        }

        private async Task GetRegions()
        {
            var response = await RegionsRepository.GetRegionsPaging(_regionsParameter);
            RegionsList = response.Items;
            MetaDataR = response.MetaData;
        }

        private async Task GetCountry()
        {
            var response = await CountryRepository.GetCountryPaging(_countryParameters);
            CountryList = response.Items;
            MetaDataC = response.MetaData;
        }

        private async Task GetProvinces()
        {
            var response = await ProvincesRepository.GetProvincesPaging(_provincesParameter);
            ProvincesList = response.Items;
            MetaDataP = response.MetaData;
        }

        private async Task GetAddress()
        {
            var response = await AddressRepository.GetAddressPaging(_addressParameter);
            AddressList = response.Items;
            MetaDataA = response.MetaData;
        }

        private string _selectedRegionName { get; set; }
        private async Task SelectedRegion(int id)
        {
            _countryParameters.RegionId = id.ToString();
            _selectedRegionName = RegionsList.FirstOrDefault(r => r.regionCode.Equals(id)).RegionName;
            _countryCreateDto.CountryRegionId = id;
            await GetCountry();
            StateHasChanged();
        }

        private string _selectedCountryName { get; set; }
        private async Task SelectedCountry(int id)
        {
            _provincesParameter.CountryId = id.ToString();
            _selectedCountryName = CountryList.FirstOrDefault(r => r.CountryId.Equals(id)).CountryName;
            _provincesCreateDto.ProvCountryId = id;
            await GetProvinces();
            StateHasChanged();
        }
        private string _selectedProvincesName { get; set; }
        private async Task SelectedProvinces(int id)
        {
            _addressParameter.ProvId = id.ToString();
            _selectedProvincesName = ProvincesList.FirstOrDefault(r => r.ProvId.Equals(id)).ProvName;
            _addressCreateDto.AddrProvId = id;
            await GetAddress();
            StateHasChanged();
        }


        private async Task SearchChangeR(string searchTerm)
        {
            _regionsParameter.PageNumber = 1;
            _regionsParameter.SearchTerm = searchTerm;
            await GetRegions();
        }

        private async Task SearchChangeC(string searchTerm)
        {
            _countryParameters.PageNumber = 1;
            _countryParameters.SearchTerm = searchTerm;
            await GetCountry();
        }

        private async Task SearchChangeP(string searchTerm)
        {
            _provincesParameter.PageNumber = 1;
            _provincesParameter.SearchTerm = searchTerm;
            await GetProvinces();
        }

        private async Task SearchChangeA(string searchTerm)
        {
            _addressParameter.PageNumber = 1;
            _addressParameter.SearchTerm = searchTerm;
            await GetAddress();
        }

        private async Task CreateRegions()
        {
            await RegionsHttp.CreateRegions(_regionsCreateDto);
            _notification.Show("/locations");
            _regionsCreateDto = new();
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();
        }

        private async Task CreateCountry()
        {
            await CountryHttp.CreateCountry(_countryCreateDto);
            _notification.Show("/locations");
            _countryCreateDto = new();
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();        }

        private async Task CreateProvinces()
        {
            await ProvincesHttp.CreateProvinces(_provincesCreateDto);
            _notification.Show("/locations");
            _provincesCreateDto = new();
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();        }

        private async Task CreateAddress()
        {
            await AddressHttp.CreateAddress(_addressCreateDto);
            _notification.Show("/locations");
            _addressCreateDto = new();
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();        
        }


        private async Task deleteRegions(int id)
        {
            await RegionsRepository.deleteRegions(id);
            _regionsParameter.PageNumber = 1;
            await GetRegions();
            await GetCountry();
            await GetProvinces();
            await GetAddress();
            _notification.Show("/locations");
        }
        
        private async Task deleteCountry(int id)
        {
            await CountryRepository.deleteCountry(id);
            _countryParameters.PageNumber = 1;
            await GetCountry();
            await GetProvinces();
            await GetAddress();
            _notification.Show("/locations");
        }

        private async Task deleteProvinces(int id)
        {
            await ProvincesRepository.deleteProvinces(id);
            _provincesParameter.PageNumber = 1;
            await GetProvinces();
            await GetAddress();
            _notification.Show("/locations");
        }
        
        private async Task deleteAddress(int id)
        {
            await AddressRepository.deleteAddress(id);
            _addressParameter.PageNumber = 1;
            await GetAddress();
            _notification.Show("/locations");
        }
    }
}