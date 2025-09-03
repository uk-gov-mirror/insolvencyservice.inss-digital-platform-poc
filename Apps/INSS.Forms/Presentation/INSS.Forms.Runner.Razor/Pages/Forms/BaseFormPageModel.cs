using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Text.Json;

public abstract class BaseFormPageModel<TForm> : PageModel where TForm : FormBase, new()
{
    private readonly IConfiguration _configuration; 
    private readonly IFormMetadataService _formMetadataService;
    private readonly IFormApiClient _formApiClient;

    protected string SessionKey { get; set; } = string.Empty;


    [BindProperty]
    required public TForm Form { get; set; }

    protected BaseFormPageModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
    {
        _configuration = configuration;
        _formMetadataService = formMetadataService;
        _formApiClient = formApiClient;
    }

    protected TForm GetFormFromSession()
    {
        var json = HttpContext.Session.GetString(SessionKey);
        return string.IsNullOrEmpty(json) ? new TForm() : JsonSerializer.Deserialize<TForm>(json)!;
    }

    protected void SaveFormToSession(TForm form)
    {
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Form));
    }

    protected async Task SaveFormToDatabase()
    {
        await _formApiClient.PostFormDataAsync(Form, _configuration["FormsApi-Url"]!);
    }
    protected void InitializeForm()
    {
        Form = GetFormFromSession();

        var formMetadata = _formMetadataService.CreateFromQueryString();
        if (Form != null && formMetadata != null && Form.FormMetadata?.FormSetInstanceId != formMetadata.FormSetInstanceId)
        {
            // This is session data is from a different form instance, so discard it and start a new form.
            Form = new TForm();
        }

        if (Form!.FormMetadata == null)
        {
            Form.FormMetadata = formMetadata!;
            SaveFormToSession(Form);
        }
    }

    protected async Task<IActionResult> ContinueToNextAction(TForm form)
    {
        // Validate, but only the items on the page
        if (!ModelState.IsValid)
        {
            Form = GetFormFromSession();
            return Page();
        }

        form.PageIndex++;
        Form = form;
        SaveFormToSession(Form);

        // End of form so Save to Cosmos and return to the Launcher.
        if (Form.PageIndex > 3)
        {
            await SaveFormToDatabase();
            return Redirect(Form.FormMetadata.ReturnUrl);
        }

        return RedirectToPage();
    }

    protected IActionResult ContinueToNextAction2(TForm form, string page)
    {
        // Validate, but only the items on the page
        if (!ModelState.IsValid)
        {
            Form = GetFormFromSession();
            return Page();
        }

        Form = form;
        SaveFormToSession(Form);

        return RedirectToPage(page);
    }

    protected async Task<IActionResult> ContinueToSave(TForm form)
    {
        // Validate, but only the items on the page
        if (!ModelState.IsValid)
        {
            Form = GetFormFromSession();
            return Page();
        }

        Form = form;
        SaveFormToSession(Form);

        await SaveFormToDatabase();

        return Redirect(Form.FormMetadata.ReturnUrl);
    }

    protected void AssignAndValidate(ModelStateDictionary modelState, TForm form, string property, object value)
    {
        ModelStateHelpers.OnlyValidateProperty(modelState, property);
        ModelStateHelpers.SetPropertyValueByName(form, property, value);
    }
}