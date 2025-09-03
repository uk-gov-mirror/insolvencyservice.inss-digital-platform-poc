using INSS.Forms.Runner.Razor.Pages.Forms.AboutYou;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou2
{
    public class AboutYouEmailModel : BaseFormPageModel<Models.Forms.AboutYou>
    {

        public string Email { get; set; } = string.Empty;

        public AboutYouEmailModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(AboutYouModel);
        }

        public void OnGet()
        {
            InitializeForm();
        }

        public async Task<IActionResult> OnPost()
        {
            var SavedForm = GetFormFromSession();

            AssignAndValidate(ModelState, SavedForm, nameof(Form.Email), Form.Email);

            return await ContinueToSave(SavedForm);
        }
    }
}