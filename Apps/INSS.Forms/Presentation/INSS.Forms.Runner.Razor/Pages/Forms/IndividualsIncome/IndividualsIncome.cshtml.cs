using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Configuration;
using INSS.Forms.Domain.Models.Enums;
using INSS.Forms.RCL.Models;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{

    /// <summary>
    /// Page model for handling the Individuals Income form workflow.
    /// </summary>
    public class IndividualsIncomeModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndividualsIncomeModel"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration settings.</param>
        /// <param name="formApiClient">The client used to interact with the form API.</param>
        /// <param name="formMetadataService">The service used to retrieve and manage form metadata.</param>
        public IndividualsIncomeModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        /// <summary>
        /// Gets or sets the income item associated with the current operation.
        /// </summary>
        [BindProperty]
        public Income IncomeItem { get; set; } = null!;

        /// <summary>
        /// Represents a collection of radio button options for selecting an income type.
        /// </summary>
        public List<RadioOption<string>> IncomeTypeOptions = EnumHelper.ToRadioOptions<IncomeType>();

        /// <summary>
        /// Represents a collection of radio button options for selecting an income frequency.
        /// </summary>
        public List<RadioOption<string>> IncomeFrequencyOptions = EnumHelper.ToRadioOptions<IncomeFrequencyType>();

        /// <summary>
        /// Handles GET requests for the page and initializes the Individuals Income Form.
        /// </summary>
        /// <returns>
        /// The current form page or redirects to the error page if the data is invalid.
        /// </returns>
        public IActionResult OnGet()
        {
            var result = InitializeForm();

            InitializeIncomeItem();

            return result;
        }

        /// <summary>
        /// Handles the HTTP POST request for progressing through the multi-step form workflow.
        /// </summary>
        /// <remarks>
        /// This method processes the current step of the form based on the value of <see cref="PageIndex"/>.
        /// It validates the model state and determines the next step or redirects to a specific page if the workflow is complete.
        /// An exception is thrown if the <see cref="PageIndex"/> is invalid.
        /// </remarks>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the POST operation.
        /// </returns>
        public IActionResult OnPost()
        {
            switch (PageIndex)
            {
                case 0:
                    return Next(ModelState, nameof(Income.IncomeType), IncomeItem.IncomeType);
                case 1:
                    return Next(ModelState, nameof(Income.Amount), IncomeItem.Amount);
                case 2:
                    return Next(ModelState, nameof(Income.IncomeFrequency), IncomeItem.IncomeFrequency);
                case 3:
                    return Next(ModelState, nameof(Income.Provider), IncomeItem.Provider, "./IndividualsIncomeList");
                default:
                    throw new InvalidOperationException("Invalid page index");
            }
        }

        /// <summary>
        /// Validates a single property of the income item, updates the form, and redirects to the next page.
        /// </summary>
        /// <param name="modelState">The model state dictionary for validation.</param>
        /// <param name="property">The name of the property to validate.</param>
        /// <param name="value">The value to set for the property.</param>
        /// <param name="nextPage">The next page to redirect to. If empty, stays on the current page.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation.
        /// </returns>
        protected IActionResult Next(ModelStateDictionary modelState, string property, object? value, string nextPage = "")
        {
            ModelStateHelpers.OnlyValidateProperty(modelState, property);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoadFormFromSession();

            var formIncome = Form.Income[ItemIndex];
            ModelStateHelpers.SetPropertyValueByName(formIncome, property, value);

            SaveFormToSession(Form);

            PageIndex++;

            return RedirectToPage(nextPage);
        }

        /// <summary>
        /// Ensures that the <see cref="Form.Income"/> collection is initialized and contains at least the number of
        /// entries required to include the item at the current <see cref="ItemIndex"/>.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Form.Income"/> collection is empty or does not have enough entries
        /// to include the item at <see cref="ItemIndex"/>, new <see cref="Income"/> instances are added to the
        /// collection until the required size is reached. The updated form is then saved to the session.
        /// </remarks>
        private void InitializeIncomeItem()
        {
            if (!Form.Income.Any() || Form.Income.Count <= ItemIndex)
            {
                // Add incomes until the list contains an entry for ItemIndex
                while (Form.Income.Count <= ItemIndex)
                {
                    Form.Income.Add(new Income());
                }

                SaveFormToSession(Form);
            }

            IncomeItem = Form.Income[ItemIndex];
        }
    }
}
