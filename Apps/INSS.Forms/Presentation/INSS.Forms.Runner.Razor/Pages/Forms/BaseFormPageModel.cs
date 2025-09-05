using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
public abstract class BaseFormPageModel<TForm> : PageModel where TForm : FormBase, new()
{
    private readonly IConfiguration _configuration; 
    private readonly IFormMetadataService _formMetadataService;
    private readonly IFormApiClient _formApiClient;

    /// <summary>
    /// Gets or sets the session key used to identify the current session.
    /// </summary>
    protected string SessionKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the index of the current page stored in the session.
    /// </summary>
    public int PageIndex
    {
        get
        {
            var value = HttpContext.Session.GetInt32($"{SessionKey}_PageIndex");
            return value ?? 0;
        }
        set
        {
            HttpContext.Session.SetInt32($"{SessionKey}_PageIndex", value);
        }
    }

    /// <summary>
    /// Gets or sets the list index of the current item stored in the session.
    /// </summary>
    public int ItemIndex
    {
        get
        {
            var value = HttpContext.Session.GetInt32($"{SessionKey}_ItemIndex");
            return value ?? 0;
        }
        set
        {
            HttpContext.Session.SetInt32($"{SessionKey}_ItemIndex", value);
        }
    }


    /// <summary>
    /// Gets or sets the form data associated with the current request.
    /// </summary>
    [BindProperty]
    required public TForm Form { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseFormPageModel"/> class with the specified configuration, API
    /// client, and metadata service. 
    /// </summary>
    /// <param name="configuration">The application configuration settings.</param>
    /// <param name="formApiClient">The client used to interact with the form API.</param>
    /// <param name="formMetadataService">The service used to retrieve and manage form metadata.</param>
    protected BaseFormPageModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
    {
        _configuration = configuration;
        _formMetadataService = formMetadataService;
        _formApiClient = formApiClient;
    }

    /// <summary>
    /// Handles when a user clicks the back button on a form page.
    /// </summary>
    /// <param name="pageName">Redirects to a specific page rather than the current one.</param>
    public IActionResult OnPostBack(string pageName = "")
    {
        if (PageIndex > 0)
        {
            PageIndex--;
        }

        return RedirectToPage(pageName);
    }

    /// <summary>
    /// Loads the form data from the current session and deserializes it into the <see cref="Form"/> property.
    /// </summary>
    /// <remarks>
    /// If no session data is found or the session data is empty, a new instance of <typeparamref
    /// name="TForm"/> is created and assigned to the <see cref="Form"/> property.
    /// </remarks>
    protected void LoadFormFromSession()
    {
        var json = HttpContext.Session.GetString(SessionKey);

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
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Form));
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
        return await _formApiClient.PostFormDataAsync(Form, _configuration["FormsApi-Url"]!);
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
    protected IActionResult InitializeForm()
    {
        LoadFormFromSession();

        var formMetadata = _formMetadataService.CreateFromQueryString();

        // If metadata is present in the query, treat this as the first call from the launcher.
        if (formMetadata is not null)
        {
            ItemIndex = 0;
            PageIndex = 0;

            // If this is a different form set instance, start a new form.
            if (Form.FormMetadata?.FormSetInstanceId != formMetadata.FormSetInstanceId)
            {
                Form = new TForm();
            }

            // Always update metadata from the query on first call.
            Form.FormMetadata = formMetadata;
            SaveFormToSession(Form);
            
            return Page();
        }

        return ValidateMetadata();
    }

    /// <summary>
    /// Validates that the form metadata is present.  If not, redirects to an error page.
    /// </summary>
    /// <returns>
    /// The current page if the form metadata is present; otherwise, a redirect to the error page.
    /// </returns>
    private IActionResult ValidateMetadata()
    {
        if (Form?.FormMetadata is null)
        {
            TempData["ErrorCode"] = "MissingFormMetadata";

            TempData["ErrorTitle"] = "Missing Form Metadata";

            TempData["ErrorMessage"] = "The form metadata is missing from the query string, you must navigate to this page via the Forms Launcher.";

            return RedirectToPage("/Error");
        }

        return Page();
    }
}