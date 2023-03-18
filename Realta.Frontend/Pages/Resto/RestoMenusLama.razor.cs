//using Microsoft.AspNetCore.Components;
//using Realta.Contract.Models;
//using Realta.Frontend.HttpRepository;

//namespace Realta.Frontend.Pages.Resto
//{
//    public partial class RestoMenus
//    {
//        [Parameter]
//        public int id
//        {
//            get;
//            set;
//        }
//        [Inject]
//        public IRestoMenusHttpRepository RestoMenusRepo { get; set; }
//        public List<RestoMenusDto> RestoMenusList { get; set; } = new List<RestoMenusDto>();
        
//        protected async override Task OnInitializedAsync()
//    {
//        RestoMenusList = await RestoMenusRepo.GetProducts();

//    }

//    }
//}
