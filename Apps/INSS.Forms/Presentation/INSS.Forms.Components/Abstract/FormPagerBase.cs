using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Provides base functionality for paginated forms in Blazor, including navigation and page visibility logic.
    /// </summary>
    public abstract class FormPagerBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the <see cref="NavigationManager"/> used for navigation between pages.
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// Determines whether the specified page is currently visible.
        /// </summary>
        /// <param name="pageName">The name of the page to check.</param>
        /// <returns><c>true</c> if the page is currently visible; otherwise, <c>false</c>.</returns>
        public bool PageVisibility(string pageName)
        {
            int index = Array.IndexOf(PageNames, pageName);
            return index == currentPageIndex;
        }

        /// <summary>
        /// Gets an <see cref="EventCallback"/> that triggers the <see cref="Next"/> method.
        /// </summary>
        public EventCallback OnNext => EventCallback.Factory.Create(this, Next);

        /// <summary>
        /// Navigates to the next page or to a specific page if a page name is provided.
        /// If on the last page, navigates to a predefined URL.
        /// </summary>
        /// <param name="pageName">Optional. The name of the page to navigate to.</param>
        public async Task Next(string pageName = "")
        {
            if (pageName.Length > 0)
            {
                int index = Array.IndexOf(PageNames, pageName);
                if (index >= 0)
                {
                    currentPageIndex = index;
                    await InvokeAsync(StateHasChanged);
                    return;
                }
            }

            if (currentPageIndex == PageNames.Length - 1)
            {
                NavigationManager?.NavigateTo("https://dro.local:6001/", forceLoad: true);
            }
            if (currentPageIndex < PageNames.Length - 1)
            {
                currentPageIndex++;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Navigates to the next page in the sequence.
        /// </summary>
        public async Task Next()
        {
            await Next(string.Empty);
        }

        /// <summary>
        /// Navigates to the previous page in the sequence, if not already at the first page.
        /// </summary>
        public async Task Back()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Gets or sets the array of page names representing the form pages.
        /// </summary>
        public string[] PageNames { get; set; } = [];

        /// <summary>
        /// Gets a value indicating whether the current page is the last page.
        /// </summary>
        public bool IsLastPage => currentPageIndex == PageNames.Length - 1;

        /// <summary>
        /// The index of the currently visible page.
        /// </summary>
        private int currentPageIndex = 0;
    }
}
