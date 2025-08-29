namespace INSS.Forms.Components.Services
{
    /// <summary>
    /// Defines navigation functionality for multi-page forms, including page transitions and visibility logic.
    /// </summary>
    public interface IFormPageNavigatorService
    {
        /// <summary>
        /// Occurs when the current page changes.
        /// </summary>
        event Action OnPageChange;

        /// <summary>
        /// Occurs when the form is completed and the data is ready to be saved.
        /// </summary>
        event Func<Task> OnReadyToSave;

        /// <summary>
        /// Gets or sets the list of page names in the form.
        /// </summary>
        List<string> PageNames { get; set; }

        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        int CurrentPageIndex { get; set; }

        /// <summary>
        /// Gets the name of the current page being displayed.
        /// </summary>
        string CurrentPageName { get; }

        /// <summary>
        /// Gets a value indicating whether the current page is the first page.
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Gets a value indicating whether the current page is the last page.
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Determines whether the specified page name matches the current page.
        /// </summary>
        /// <param name="pageName">The name of the page to check.</param>
        /// <returns><see langword="true"/> if the specified page name matches the current page; otherwise, <see
        /// langword="false"/>.</returns>
        public bool IsCurrentPage(string pageName);


        /// <summary>
        /// Determines whether the specified page is visible.
        /// </summary>
        /// <param name="pageName">The name of the page to check visibility for.</param>
        /// <param name="forceHide">If set to <c>true</c>, forces the page to be hidden regardless of other logic.</param>
        /// <returns><c>true</c> if the page is visible; otherwise, <c>false</c>.</returns>
        bool PageVisibility(string pageName, bool forceHide = false);

        /// <summary>
        /// Navigates to the previous page.
        /// </summary>
        void PreviousPage();

        /// <summary>
        /// Navigates to the next page, optionally specifying the page name.
        /// </summary>
        /// <param name="pageName">The name of the next page to navigate to. If not specified, navigates to the next sequential page.</param>
        void NextPage(string pageName = "");

        /// <summary>
        /// Navigates to the next page.
        /// </summary>
        void NextPage();

        /// <summary>
        /// Submits the form data and completes the form process.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SubmitFormAsync();
    }
}
