using BlazorHybrid.Core;

namespace BlazorHybrid.Components.Pages
{
    public partial class Home
    {
        private List<BlazorData> blazorData = new List<BlazorData>();

        protected override async Task OnInitializedAsync()
        {
            blazorData = dataService.GetMockData();
        }
    }
}
