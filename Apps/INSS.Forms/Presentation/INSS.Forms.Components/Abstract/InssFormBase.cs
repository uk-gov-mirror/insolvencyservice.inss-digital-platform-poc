using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace INSS.Forms.Components.Abstract
{

    /// <summary>
    /// Serves as the base class for INSS forms, providing common functionality for form initialization, navigation, and
    /// data handling in a Blazor application.
    /// </summary>
    /// <remarks>This abstract class is designed to be inherited by specific form implementations. It provides
    /// dependency-injected services for navigation, configuration, metadata retrieval, and API communication. Derived
    /// classes can use these services to manage form lifecycle events, such as initialization, page navigation, and
    /// data submission.</remarks>
    public abstract class InssFormBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the navigation service for managing form page navigation and form data.
        /// </summary>
        [Inject] protected IFormPageNavigatorService FormPageNavigationService { get; set; } = default!;

        /// <summary>
        /// Gets or sets the Blazor <see cref="NavigationManager"/> for page navigation.
        /// </summary>
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

        /// <summary>
        /// Gets or sets the application configuration service.
        /// </summary>
        [Inject] protected IConfiguration Configuration { get; set; } = default!;

        /// <summary>
        /// Gets or sets the service for retrieving form instance metadata.
        /// </summary>
        [Inject] protected IFormMetadataService FormMetadata { get; set; } = default!;

        /// <summary>
        /// Gets or sets the API client for posting form data.
        /// </summary>
        [Inject] protected IFormApiClient ApiClient { get; set; } = default!;

        /// <summary>
        /// Holds the current form data instance.
        /// </summary>
        private FormBase? _formData;

        /// <summary>
        /// Initializes the form metadata and sets the form data in the navigation service.
        /// Subscribes to page change and ready-to-save events.
        /// </summary>
        /// <typeparam name="T">The type of the form, derived from <see cref="FormBase"/>.</typeparam>
        /// <param name="form">The form instance to initialize.</param>
        protected void OnInitialized<T>(T form) where T : FormBase
        {
            form.InitializeMetadata(FormMetadata.CreateFromQueryString());
            _formData = form;

            //FormPageNavigationService.OnPageChange += HandlePageChange;
            //FormPageNavigationService.OnReadyToSave += HandleReadyToSaveAsync;

            base.OnInitialized();
        }

        /// <summary>
        /// Handles the page change event by triggering a UI refresh.
        /// </summary>
        private void HandlePageChange()
        {
            StateHasChanged();
        }

        /// <summary>
        /// Handles the ready-to-save event by posting form data to the API and navigating to the return URL if successful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task HandleReadyToSaveAsync()
        {
            bool success = await ApiClient.PostFormDataAsync(_formData!, $"{Configuration["FormsApi-Url"]}").ConfigureAwait(false);

            if (success)
            {
                NavigationManager?.NavigateTo(_formData!.FormMetadata.ReturnUrl, forceLoad: true);
            }
        }
    }
}
