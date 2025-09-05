using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{
    /// <summary>
    /// Page model for the Individuals Income Summary page.
    /// Handles initialization, form submission, and navigation for the summary of individual incomes.
    /// </summary>
    public class IndividualsIncomeSummaryModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndividualsIncomeSummaryModel"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration settings.</param>
        /// <param name="formApiClient">The client used to interact with the form API.</param>
        /// <param name="formMetadataService">The service used to retrieve and manage form metadata.</param>
        public IndividualsIncomeSummaryModel(
            IConfiguration configuration,
            IFormApiClient formApiClient,
            IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        /// <summary>
        /// Handles GET requests to initialize the form and display the summary page.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the initialization process.
        /// </returns>
        public IActionResult OnGet()
        {
            return InitializeForm();
        }

        /// <summary>
        /// Handles POST requests to save the form data to the database.
        /// Redirects to the return URL on success, or redisplays the page on failure.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        public async Task<IActionResult> OnPost()
        {
            LoadFormFromSession();

            if (await SaveFormToDatabase())
            {
                return Redirect(Form.FormMetadata.ReturnUrl);
            }
            else
            {
                return Page();
            }
        }

        /// <summary>
        /// Handles POST requests to change an income item.
        /// Sets the current item and page index, then redirects to the Individuals Income page.
        /// </summary>
        /// <param name="itemIndex">The index of the income item to change.</param>
        /// <param name="pageIndex">The index of the page to redirect to.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> that redirects to the Individuals Income page.
        /// </returns>
        public IActionResult OnPostChange(int itemIndex, int pageIndex)
        {
            ItemIndex = itemIndex;
            PageIndex = pageIndex;

            return RedirectToPage("./IndividualsIncome");
        }
    }
}
