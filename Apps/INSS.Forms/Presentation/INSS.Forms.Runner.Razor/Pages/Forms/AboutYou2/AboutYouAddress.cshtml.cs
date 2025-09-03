using INSS.Forms.Runner.Razor.Pages.Forms.AboutYou;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou2
{
    public class AboutYouAddressModel : BaseFormPageModel<Models.Forms.AboutYou>
    {

        public Models.Composite.Address Address { get; set; } = new ();

        public AboutYouAddressModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
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

            AssignAndValidate(ModelState, SavedForm, nameof(Form.Address), Form.Address!);

            return ContinueToNextAction2(SavedForm, "/Forms/AboutYou2/AboutYouTelephone");
        }
    }
}
