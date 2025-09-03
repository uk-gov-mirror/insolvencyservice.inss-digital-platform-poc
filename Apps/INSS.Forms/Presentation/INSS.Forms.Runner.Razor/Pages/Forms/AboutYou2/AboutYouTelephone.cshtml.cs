using INSS.Forms.Runner.Razor.Pages.Forms.AboutYou;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou2
{
    public class AboutYouTelephoneModel : BaseFormPageModel<Models.Forms.AboutYou>
    {

        public string Telephone { get; set; } = string.Empty;

        public AboutYouTelephoneModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(AboutYouModel);
        }

        public void OnGet()
        {
            InitializeForm();
        }

        public IActionResult OnPost()
        {
            var SavedForm = GetFormFromSession();

            AssignAndValidate(ModelState, SavedForm, nameof(Form.Telephone), Form.Telephone);

            return ContinueToNextAction2(SavedForm, "/Forms/AboutYou2/AboutYouEmail");
        }
    }
}