using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Configuration;
using INSS.Forms.Domain.Models.Enums;
using INSS.Forms.RCL.Models;
using INSS.Forms.Runner.Razor.Pages.Forms.AboutYou;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models = INSS.Forms.Domain.Models;

namespace INSS.Forms.Runner.Razor.Pages.Forms.IndividualsIncome
{
    public class IndividualsIncomeModel : BaseFormPageModel<Models.Forms.IndividualsIncome>
    {
        public IndividualsIncomeModel(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService)
            : base(configuration, formApiClient, formMetadataService)
        {
            SessionKey = nameof(IndividualsIncomeModel);
        }

        [BindProperty]
        public Income IncomeItem { get; set; } = null!;


        public List<RadioOption<string>> IncomeTypeOptions = EnumHelper.ToRadioOptions<IncomeType>();
        public List<RadioOption<string>> IncomeFrequencyOptions = EnumHelper.ToRadioOptions<IncomeFrequencyType>();



        public void OnGet()
        {
            InitializeForm();

            InitializeIncomeItem();
        }

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

        protected IActionResult Next(ModelStateDictionary modelState, string property, object? value, string nextPage = "")
        {
            ModelStateHelpers.OnlyValidateProperty(modelState, property);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Form = GetFormFromSession();
            var formIncome = Form.Income[ItemIndex];
            ModelStateHelpers.SetPropertyValueByName(formIncome, property, value);
            SaveFormToSession(Form);

            PageIndex++;

            return RedirectToPage(nextPage);
        }

        //protected IActionResult IfValidShowList(string page)
        //{
        //    // Validate, but only the items on the page
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    return RedirectToPage(page);
        //}



        //protected void AssignAndValidate(ModelStateDictionary modelState, Income income, string property, object? value)
        //{
        //    ModelStateHelpers.OnlyValidateProperty(modelState, property);
        //    //ModelStateHelpers.SetPropertyValueByName(income, property, value);
        //}


        /// <summary>
        /// Ensures that the <see cref="Form.Income"/> collection is initialized and contains at least the number of
        /// entries required to include the item at the current <see cref="ItemIndex"/>.
        /// </summary>
        /// <remarks>If the <see cref="Form.Income"/> collection is empty or does not have enough entries
        /// to include the item at <see cref="ItemIndex"/>, new <see cref="Income"/> instances are added to the
        /// collection until the required size is reached. The updated form is then saved to the session.</remarks>
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
