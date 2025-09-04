using INSS.Forms.Application.Common.Extensions;
using INSS.Forms.Domain.Models.Enums;
using INSS.Forms.RCL.Models;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{
    public class IndividualsIncomeListModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        public IndividualsIncomeListModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        public List<RadioOption<string>> ConfirmOptions = EnumHelper.ToRadioOptions<ConfirmType>();

        [BindProperty]
        public string? AddNew { get; set; } = null;

        public void OnGet()
        {
            InitializeForm();
        }

        public IActionResult OnPost()
        {
            if (AddNew == ConfirmType.Yes.GetDescription())
            {
                PageIndex = 0;
                ItemIndex++;
                return RedirectToPage("./IndividualsIncome");
            }
            else if (AddNew == ConfirmType.No.GetDescription())
            {
                return RedirectToPage("./IndividualsIncomeSummary");
            }
            else
            {
                return Page();
            }
        }
    }
}
