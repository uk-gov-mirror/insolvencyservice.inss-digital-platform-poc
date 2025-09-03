using INSS.Forms.Runner.Razor.Pages.Forms.AboutYou;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou2
{
    public class AboutYouNameModel : BaseFormPageModel<Models.Forms.AboutYou>
    {

        public string Name { get; set; } = string.Empty;

        public AboutYouNameModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
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

            AssignAndValidate(ModelState, SavedForm, nameof(Form.Name), Form.Name);

            return ContinueToNextAction2(SavedForm, "/Forms/AboutYou2/AboutYouAddress");
        }
    }
}
