using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.AboutYou
{
    public class AboutYouModel : BaseFormPageModel<Models.Forms.AboutYou>
    {
        public AboutYouModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
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
            var savedForm = GetFormFromSession();

            switch (PageIndex)
            {
                case 0:
                    AssignAndValidate(ModelState, savedForm, nameof(Form.Name), Form.Name);
                    break;
                case 1:
                    AssignAndValidate(ModelState, savedForm, nameof(Form.Address), Form.Address!);
                    break;
                case 2:
                    AssignAndValidate(ModelState, savedForm, nameof(Form.Telephone), Form.Telephone);
                    break;
                case 3:
                    AssignAndValidate(ModelState, savedForm, nameof(Form.Email), Form.Email);
                    break;
            }

            return await IfValidNextPage(savedForm, PageIndex == 3);
        }
    }
}
