using INSS.Forms.BCL.Services;
using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace INSS.Forms.BCL.Abstract
{


    /// <summary>
    /// Provides a base class for form-based Razor Page models, enabling session management, form data handling, and
    /// integration with form metadata and API services.
    /// </summary>
    /// <remarks>This class is designed to simplify the implementation of form-based workflows in Razor Pages by
    /// providing common functionality such as session-based form state management, metadata handling, and API integration.
    /// Derived classes can use the provided methods and properties to manage form data and implement custom
    /// behavior.</remarks>
    /// <typeparam name="TForm">The type of the form associated with the page model. Must inherit from <see cref="FormBase"/> and have a
    /// parameterless constructor.</typeparam>
    public abstract class BaseFormPageModel<TForm> : ComponentBase where TForm : FormBase, new()
    {
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
        [Inject] protected IFormMetadataService FormMetadataService { get; set; } = default!;

        /// <summary>
        /// Gets or sets the API client for posting form data.
        /// </summary>
        [Inject] protected IFormApiClient ApiClient { get; set; } = default!;

        [Inject] protected IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        /// <summary>
        /// Gets or sets the session key used to identify the current session.
        /// </summary>
        protected string SessionKey => "INSS-Forms-Runner-Session";

        /// <summary>
        /// Gets or sets the index of the current page stored in the session.
        /// </summary>
        public int PageIndex
        {
            get
            {
                var value = HttpContextAccessor.HttpContext!.Session.GetInt32($"{SessionKey}_PageIndex");
                return value ?? 0;
            }
            set
            {
                HttpContextAccessor.HttpContext!.Session.SetInt32($"{SessionKey}_PageIndex", value);
            }
        }

        /// <summary>
        /// Gets or sets the list index of the current list item stored in the session.
        /// </summary>
        public int ListIndex
        {
            get
            {
                var value = HttpContextAccessor.HttpContext!.Session.GetInt32($"{SessionKey}_ListIndex");
                return value ?? 0;
            }
            set
            {
                HttpContextAccessor.HttpContext!.Session.SetInt32($"{SessionKey}_ListIndex", value);
            }
        }


        /// <summary>
        /// Gets or sets the form data associated with the current request.
        /// </summary>
        [BindProperty]
        [SupplyParameterFromForm(FormName = "aboutYouForm")]
        required public TForm Form { get; set; }

        /// <summary>
        /// Handles when a user clicks the back button on a form page.
        /// </summary>
        /// <param name="pageName">Redirects to a specific page rather than the current one.</param>
        //public IActionResult OnPostBack(string pageName = "")
        //{
        //    if (PageIndex > 0)
        //    {
        //        PageIndex--;
        //    }

        //    return RedirectToPage(pageName);
        //}

        /// <summary>
        /// Loads the form data from the current session and deserializes it into the <see cref="Form"/> property.
        /// </summary>
        /// <remarks>
        /// If no session data is found or the session data is empty, a new instance of <typeparamref
        /// name="TForm"/> is created and assigned to the <see cref="Form"/> property.
        /// </remarks>
        protected void LoadFormFromSession()
        {
            var json = HttpContextAccessor.HttpContext!.Session.GetString(SessionKey);

            Form = string.IsNullOrEmpty(json) ? new TForm() : JsonSerializer.Deserialize<TForm>(json)!;
        }

        /// <summary>
        /// Saves the specified form to the current session.
        /// </summary>
        /// <remarks>The form is serialized to JSON and stored in the session using a predefined session key.
        /// </remarks>
        /// <param name="form">The form object to be saved.</param>
        protected void SaveFormToSession(TForm form)
        {
            Form = form;
            HttpContextAccessor.HttpContext!.Session.SetString(SessionKey, JsonSerializer.Serialize(Form));
        }

        /// <summary>
        /// Saves the current form data to the database asynchronously.
        /// </summary>
        /// <remarks>This method sends the form data to the configured Forms API endpoint using an HTTP POST
        /// request.
        /// </remarks>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the form data 
        /// was successfully saved; otherwise, <see langword="false"/>.</returns>
        protected async Task<bool> SaveFormToDatabase()
        {
            return await ApiClient.PostFormDataAsync(Form, Configuration["FormsApi-Url"]!);
        }

        /// <summary>
        /// Initializes the form by retrieving or creating a new form instance and updating its metadata.
        /// </summary>
        /// <remarks>This method retrieves the form from the session and updates its metadata based on the query
        /// string. If metadata is present in the query string, it is treated as the first call from the launcher, and a new
        /// form instance is created if the form set instance ID differs. The updated form is saved back to the session. If
        /// no metadata is found, the method validates the existing metadata.</remarks>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the initialization process.  Returns a page if
        /// metadata is present in the query string; otherwise, returns the result of metadata validation.</returns>
        protected void InitializeForm()
        {
            LoadFormFromSession();

            var formMetadata = FormMetadataService.CreateFromQueryString();

            // If metadata is present in the query, treat this as the first call from the launcher.
            if (formMetadata is not null)
            {
                ListIndex = 0;
                PageIndex = 0;

                // If this is a different form set instance, start a new form.
                if (Form.FormMetadata?.FormSetInstanceId != formMetadata.FormSetInstanceId)
                {
                    Form = new TForm();
                }

                // Always update metadata from the query on first call.
                Form.FormMetadata = formMetadata;
                SaveFormToSession(Form);
            }

            ValidateMetadata();
        }

        /// <summary>
        /// Validates that the form metadata is present.  If not, redirects to an error page.
        /// </summary>
        /// <returns>
        /// The current page if the form metadata is present; otherwise, a redirect to the error page.
        /// </returns>
        private void ValidateMetadata()
        {
            if (Form?.FormMetadata is null)
            {
                NavigationManager.NavigateTo("/Error");
            }
        }
    }
}
