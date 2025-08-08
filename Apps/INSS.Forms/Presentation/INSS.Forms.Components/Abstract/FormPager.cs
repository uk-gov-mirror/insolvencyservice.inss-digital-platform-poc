using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    public abstract class FormPager : ComponentBase
    {
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        public bool PageVisibility(string pageName)
        {
            int index = Array.IndexOf(PageNames, pageName);
            return index == currentPageIndex;
        }

        public EventCallback OnNext => EventCallback.Factory.Create(this, Next);

        public async Task Next()
        {
            if(currentPageIndex == PageNames.Length -1)
            {
                NavigationManager?.NavigateTo("https://dro.local:6001/", forceLoad: true);
            }
            if (currentPageIndex < PageNames.Length - 1)
            {
                currentPageIndex++;
            }

            await InvokeAsync(StateHasChanged);
        }

        public async Task Back()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
            }

            await InvokeAsync(StateHasChanged);
        }

        public string[] PageNames { get; set; } = [];

        public bool IsLastPage => currentPageIndex == PageNames.Length - 1;

        private int currentPageIndex = 0;
    }
}
