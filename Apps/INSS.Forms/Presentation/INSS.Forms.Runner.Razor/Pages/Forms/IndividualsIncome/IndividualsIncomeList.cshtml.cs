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

        public IActionResult OnGet()
        {
            return InitializeForm();
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

        public IActionResult OnPostChange(int itemIndex)
        {
            ItemIndex = itemIndex;
            PageIndex = 0;

            return RedirectToPage("./IndividualsIncome");
        }

        public IActionResult OnPostRemove(int itemIndex)
        {
            Form = GetFormFromSession();
            Form.Income.RemoveAt(itemIndex);
            SaveFormToSession(Form);

            if(Form.Income.Count == 0)
            {
                PageIndex = 0;
                ItemIndex = 0;
                return RedirectToPage("./IndividualsIncome");
            }
            else
            {
                ItemIndex = Form.Income.Count - 1;
                return RedirectToPage();
            }
        }
    }
}
