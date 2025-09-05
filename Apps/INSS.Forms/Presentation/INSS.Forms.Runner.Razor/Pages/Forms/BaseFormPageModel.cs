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


    [BindProperty]
    required public TForm Form { get; set; }

    protected BaseFormPageModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
    {
        _configuration = configuration;
        _formMetadataService = formMetadataService;
        _formApiClient = formApiClient;
    }
    public IActionResult OnPostBack(string pageName = "")
    {
        if (PageIndex > 0)
        {
            PageIndex--;
        }

        return RedirectToPage(pageName);
    }

    protected TForm GetFormFromSession()
    {
        var json = HttpContext.Session.GetString(SessionKey);
        return string.IsNullOrEmpty(json) ? new TForm() : JsonSerializer.Deserialize<TForm>(json)!;
    }

    protected void SaveFormToSession(TForm form)
    {
        Form = form;
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Form));
    }

    protected async Task<bool> SaveFormToDatabase()
    {
        return await _formApiClient.PostFormDataAsync(Form, _configuration["FormsApi-Url"]!);
    }

    protected IActionResult InitializeForm()
    {
        Form = GetFormFromSession();

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

    protected async Task<IActionResult> IfValidNextPage(TForm form, bool endOfForm = false)
    {
        // Validate, but only the items on the page
        if (!ModelState.IsValid)
        {
            Form = GetFormFromSession();
            return Page();
        }

        PageIndex++;
        Form = form;
        SaveFormToSession(Form);

        if (endOfForm)
        {
            if (await SaveFormToDatabase())
            { 
                return Redirect(Form.FormMetadata.ReturnUrl);
            }
        }

        return RedirectToPage();
    }

    protected IActionResult IfValidShowList(TForm form, string page)
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

        if(await SaveFormToDatabase())
        {
            return Redirect(Form.FormMetadata.ReturnUrl);
        }
        else
        {
            return Page();
        }
    }

    protected void AssignAndValidate(ModelStateDictionary modelState, TForm form, string property, object? value)
    {
        ModelStateHelpers.OnlyValidateProperty(modelState, property);
        ModelStateHelpers.SetPropertyValueByName(form, property, value);
    }

    protected void AssignAndValidate(ModelStateDictionary modelState, TForm form, string collectionPropertyName, int collectionIndex, string property, object? value)
    {
        ModelStateHelpers.OnlyValidateCollectionProperty(modelState, collectionPropertyName, collectionIndex, property);
        ModelStateHelpers.SetCollectionPropertyValueByName(form, collectionPropertyName, collectionIndex, property, value);
    }
}