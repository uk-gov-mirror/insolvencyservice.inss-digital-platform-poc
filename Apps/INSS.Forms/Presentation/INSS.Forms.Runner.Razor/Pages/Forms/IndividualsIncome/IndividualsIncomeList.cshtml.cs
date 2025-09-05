using INSS.Forms.Application.Common.Extensions;
using INSS.Forms.Domain.Models.Enums;
using INSS.Forms.RCL.Models;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{
    /// <summary>
    /// Page model for managing the list of individual income entries within the form.
    /// Handles adding, changing, and removing income entries, as well as navigation between related pages.
    /// </summary>
    public class IndividualsIncomeListModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndividualsIncomeListModel"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration settings.</param>
        /// <param name="formApiClient">The client used to interact with the form API.</param>
        /// <param name="formMetadataService">The service used to retrieve and manage form metadata.</param>
        public IndividualsIncomeListModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        /// <summary>
        /// Gets the radio button options for confirming whether to add a new income entry.
        /// </summary>
        public List<RadioOption<string>> ConfirmOptions = EnumHelper.ToRadioOptions<ConfirmType>();

        /// <summary>
        /// Gets or sets the value indicating whether the user wants to add a new income entry.
        /// </summary>
        [BindProperty]
        public string? AddNew { get; set; } = null;

        /// <summary>
        /// Handles GET requests to initialize the form and display the income list page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the initialization process.</returns>
        public IActionResult OnGet()
        {
            return InitializeForm();
        }

        /// <summary>
        /// Handles POST requests for adding a new income entry or navigating to the summary page.
        /// </summary>
        /// <returns>
        /// A redirect to the IndividualsIncome page if adding a new entry,
        /// a redirect to the IndividualsIncomeSummary page if not,
        /// or redisplays the current page if no option is selected.
        /// </returns>
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

        /// <summary>
        /// Handles POST requests to change an existing income entry.
        /// </summary>
        /// <param name="itemIndex">The zero-based index of the income entry to change.</param>
        /// <returns>A redirect to the IndividualsIncome page for editing the selected entry.</returns>
        public IActionResult OnPostChange(int itemIndex)
        {
            ItemIndex = itemIndex;
            PageIndex = 0;

            return RedirectToPage("./IndividualsIncome");
        }

        /// <summary>
        /// Handles the removal of an income entry from the individual's income list.
        /// </summary>
        /// <param name="itemIndex">The zero-based index of the income entry to remove.</param>
        /// <returns>
        /// A redirect to the appropriate page based on the remaining income entries:
        /// - If no income entries remain, redirects to the IndividualsIncome page to prompt for a new entry.
        /// - If income entries remain, updates the item index and redirects to the current page.
        /// </returns>
        public IActionResult OnPostRemove(int itemIndex)
        {
            LoadFormFromSession();
            Form.Income.RemoveAt(itemIndex);
            SaveFormToSession(Form);

            if (Form.Income.Count == 0)
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
