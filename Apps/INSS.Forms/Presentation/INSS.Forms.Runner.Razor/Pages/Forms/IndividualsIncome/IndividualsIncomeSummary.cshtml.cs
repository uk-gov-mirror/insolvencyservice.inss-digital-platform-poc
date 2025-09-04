using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{
    public class IndividualsIncomeSummaryModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        public IndividualsIncomeSummaryModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        public void OnGet()
        {
            InitializeForm();
        }

        public IActionResult OnPost()
        {
            return Page();
        }

        public IActionResult OnPostChange(int itemIndex, int pageIndex)
        {
            ItemIndex = itemIndex;
            PageIndex = pageIndex;

            return RedirectToPage("./IndividualsIncome");
        }
    }
}
