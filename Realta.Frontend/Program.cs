using Realta.Frontend;
using Realta.Frontend.HttpRepository.Purchasing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Frontend.HttpRepository.Master.CategoryGroup;
using Realta.Frontend.HttpRepository.Master;
using Realta.Frontend.HttpRepository.Master.Policy;
using Realta.Frontend.HttpRepository.Master.PriceItems;
using Realta.Frontend.HttpRepository.Master.ServiceTask;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

// booking

// hotel
// builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();

// hr

// master
builder.Services.AddScoped<ICategoryGroupHttpRepository, CategoryGroupHttpRepository>();
builder.Services.AddScoped<IPriceItemsHttpRepository, PriceItemsHttpRepository>();
builder.Services.AddScoped<IPolicyHttpRepository, PolicyHttpRepository>();
builder.Services.AddScoped<IServiceTaskHttpRepository, ServiceTaskHttpRepository>();
builder.Services.AddScoped<IRegionsHttpRepository, RegionsHttpRepository>();
builder.Services.AddScoped<ICountryHttpRepository, CountryHttpRepository>();
builder.Services.AddScoped<IProvincesHttpRepository, ProvincesHttpRepository>();
builder.Services.AddScoped<IAddressHttpRepository, AddressHttpRepository>();
// payment

// purchasing
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();
builder.Services.AddScoped<IVendorHttpRepository, VendorHttpRepository>();
builder.Services.AddScoped<IVendorProductHttpRepository, VendorProductHttpRepository>();
// builder.Services.AddScoped<IPurchaseOrderHttpRepository, PurchaseOrderHttpRepository>();
builder.Services.AddScoped<IStocksHttpRepository, StocksHttpRepository>();
builder.Services.AddScoped<IStockDetailHttpRepository, StockDetailHttpRepository>();


// resto

// users


await builder.Build().RunAsync();