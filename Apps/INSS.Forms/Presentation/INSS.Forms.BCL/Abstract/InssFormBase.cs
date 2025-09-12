using INSS.Forms.BCL.Abstract;
using INSS.Forms.BCL.Services;
using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

/// <summary>
/// Provides a base Blazor component for form handling, including session management, navigation, and form metadata.
/// </summary>
/// <typeparam name="TForm">The type of form model, which must inherit from <see cref="FormBase"/> and have a parameterless constructor.</typeparam>
public abstract class InssFormBase<TForm> : InssCommonBase where TForm : FormBase, new()
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

    /// <summary>
    /// The name of the main form used for parameter binding.
    /// </summary>
    protected const string FormName = "main-form";

    /// <summary>
    /// Gets or sets the <see cref="EditContext"/> for the current form, used for validation and editing.
    /// </summary>
    protected EditContext EditContext { get; set; } = null!;

    /// <summary>
    /// Gets or sets the form model bound to the main form.
    /// </summary>
    [SupplyParameterFromForm(FormName = FormName)]
    protected TForm Form { get; set; } = new();

    /// <summary>
    /// Initializes the form, loading from session and handling metadata from the query string.
    /// </summary>
    protected void InitializeFormInstance()
    {
        var cachedForm = RetrieveCachedFormState();

        var formMetadata = FormMetadataService.CreateFromQueryString();

        // If metadata is present in the query, treat this as the first call from the launcher.
        if (formMetadata is not null)
        {
            ListIndex = 0;
            PageIndex = 0;

            // If this is a different form set instance, start a new form.
            if (cachedForm.FormMetadata?.FormSetInstanceId != formMetadata.FormSetInstanceId)
            {
                cachedForm = new TForm();
            }

            // Always update metadata from the query on first call.
            cachedForm.FormMetadata = formMetadata;
            CacheFormState(cachedForm);

            // Remove query parameters, 
            // we do not want to keep reading them with every page load 
            // and it is also useful to indicate the very first time the form instance is created.
            var uri = NavigationManager.Uri;
            var baseUri = uri.Split('?')[0];
            NavigationManager.NavigateTo(baseUri, forceLoad: false, replace: true);
        }

        ValidateMetadata(cachedForm);

        Form = cachedForm;
    }

    /// <summary>
    /// Retrieves the current form state from cache.
    /// <para>
    /// <b>Note:</b> The form state is currently persisted in the user's session. 
    /// This implementation may be updated in the future to use a different caching solution, 
    /// such as Redis or distributed cache, to support scalability or multi-server scenarios.
    /// </para>
    /// </summary>
    /// <returns>
    /// The form model of type <typeparamref name="TForm"/> retrieved from session, or a new instance if not found.
    /// </returns>
    protected TForm RetrieveCachedFormState()
    {
        var json = HttpContextAccessor.HttpContext!.Session.GetString(Config.SessionKey);

        var form = string.IsNullOrEmpty(json) ? new TForm() : JsonSerializer.Deserialize<TForm>(json)!;

        return form;
    }

    /// <summary>
    /// Caches the current form state for later retrieval.
    /// <para>
    /// <b>Note:</b> The form is currently persisted to the user's session. 
    /// This implementation may be updated in the future to use a different caching solution, 
    /// such as Redis or distributed cache, to support scalability or multi-server scenarios.
    /// </para>
    /// </summary>
    /// <param name="form">The form model to cache.</param>
    protected void CacheFormState(TForm form)
    {
        HttpContextAccessor.HttpContext!.Session.SetString(Config.SessionKey, JsonSerializer.Serialize(form));
    }

    /// <summary>
    /// Asynchronously saves the specified form model to the database using the configured API client.
    /// </summary>
    /// <param name="form">
    /// The form model to be saved. This should be an instance of <typeparamref name="TForm"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, 
    /// with a result of <c>true</c> if the form was successfully saved to the database; otherwise, <c>false</c>.
    /// </returns>
    protected async Task<bool> SaveFormToDatabase(TForm form)
    {
        return await ApiClient.PostFormDataAsync(form, Configuration["FormsApi-Url"]!);
    }

    /// <summary>
    /// Returns the full field name for a property of the <see cref="Form"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property for which to generate the field name.</param>
    /// <returns>
    /// A string representing the full field name in the format "Form.PropertyName".
    /// </returns>
    protected override string FieldName(string propertyName)
    {
        return string.Concat(nameof(Form), ".", propertyName);
    }

    /// <summary>
    /// Validates that the form metadata is present; navigates to the error page if not.
    /// </summary>
    /// <param name="form">The form model to validate.</param>
    private void ValidateMetadata(TForm form)
    {
        if (form?.FormMetadata is null)
        {
            NavigationManager.NavigateTo("/Error");
        }
    }
}