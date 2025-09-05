using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou
{
    /// <summary>
    /// Page model for the About You form, handling initialization and postback logic.
    /// </summary>
    public class AboutYouModel : BaseFormPageModel<Models.Forms.AboutYou>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutYouModel"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration settings.</param>
        /// <param name="formApiClient">API client for form data operations.</param>
        /// <param name="formMetadataService">Service for form metadata management.</param>
        public AboutYouModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(AboutYouModel);
        }

        /// <summary>
        /// Handles GET requests to initialize the form.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the initialization.</returns>
        public IActionResult OnGet()
        {
            return InitializeForm();
        }

        /// <summary>
        /// Handles POST requests for progressing through the About You form pages.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the post operation.</returns>
        public async Task<IActionResult> OnPost()
        {
            switch (PageIndex)
            {
                case 0:
                    return await Next(ModelState, nameof(Form.Name), Form.Name);
                case 1:
                    return await Next(ModelState, nameof(Form.Address), Form.Address);
                case 2:
                    return await Next(ModelState, nameof(Form.Telephone), Form.Telephone);
                case 3:
                    return await Next(ModelState, nameof(Form.Email), Form.Email, true);
                default:
                    throw new InvalidOperationException("Invalid page index");
            }
        }

        /// <summary>
        /// Validates and saves the specified property value, optionally persisting the form to the database.
        /// </summary>
        /// <param name="modelState">The current model state for validation.</param>
        /// <param name="property">The name of the property to validate and save.</param>
        /// <param name="value">The value to set for the property.</param>
        /// <param name="saveToDatabase">Whether to save the form to the database after validation.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        protected async Task<IActionResult> Next(ModelStateDictionary modelState, string property, object? value, bool saveToDatabase = false)
        {
            ModelStateHelpers.OnlyValidateProperty(modelState, property);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoadFormFromSession();

            ModelStateHelpers.SetPropertyValueByName(Form, property, value);

            SaveFormToSession(Form);

            PageIndex++;

            if (saveToDatabase)
            {
                if (await SaveFormToDatabase())
                {
                    return Redirect(Form.FormMetadata.ReturnUrl);
                }
            }

            return RedirectToPage();
        }
    }
}
