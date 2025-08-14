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

        private LastAction lastAction = LastAction.Next;

        public enum LastAction
        {
            Back,
            Next
        }

        /// <summary>
        /// Determines whether the specified page is currently visible.
        /// </summary>
        /// <param name="pageName">The name of the page to check.</param>
        /// <param name="overrideIfTrue">Optionally pass in logic that can override a true result and always return false.</param>
        /// <returns><c>true</c> if the page is currently visible; otherwise, <c>false</c>.</returns>
        public bool PageVisibility(string pageName, Func<bool>? overrideIfTrue = null)
        {
            int index = Array.IndexOf(PageNames, pageName);
            var visible = index == currentPageIndex;

            if (visible)
            {
                if (overrideIfTrue != null && overrideIfTrue())
                {
                    visible = false;

                    if(lastAction == LastAction.Back)
                    {
                        _ = Back();
                    }
                    else
                    {
                        _ = Next();
                    }
                }
            }

            return visible;
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
            lastAction = LastAction.Next;

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
            lastAction = LastAction.Back;

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
        /// Gets a value indicating whether the current page is the first page.
        /// </summary>
        public bool IsFirstPage => currentPageIndex == 0;

        /// <summary>
        /// The index of the currently visible page.
        /// </summary>
        private int currentPageIndex = 0;
    }
}
