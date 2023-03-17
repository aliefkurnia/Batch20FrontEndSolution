using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Shared
{
    public partial class SuccessNotification
    {
        private string _modalDisplay;
        private string _modalClass;
        private bool _showBackdrop;

        [Parameter]
        public string PathRoute { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        public void Show(string path)
        {
            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            PathRoute = path;
            StateHasChanged();
        }

        private void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
            Navigation.NavigateTo(PathRoute);
        }
    }
}
