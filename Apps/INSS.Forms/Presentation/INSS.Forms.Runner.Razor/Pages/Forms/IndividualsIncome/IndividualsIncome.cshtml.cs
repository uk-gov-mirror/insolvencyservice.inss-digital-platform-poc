using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Enums;
using INSS.Forms.RCL.Models;
using INSS.Forms.Runner.Razor.Services;
using Microsoft.AspNetCore.Mvc;
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

        public List<RadioOption<string>> IncomeTypeOptions = EnumHelper.ToRadioOptions<IncomeType>();
        public List<RadioOption<string>> IncomeFrequencyOptions = EnumHelper.ToRadioOptions<IncomeFrequencyType>();

        public void OnGet()
        {
            InitializeForm();

            EnsureIncomeEntryExists();
        }

        public async Task<IActionResult> OnPost()
        {
            var savedForm = GetFormFromSession();


            if(!Form.Income.Any())
            {
                EnsureIncomeEntryExists();
                ModelState.AddModelError($"Form.Income[{ItemIndex}].IncomeType", "Income type is required.");
            }

            switch (PageIndex)
            {
                case 0:
                    AssignAndValidate(ModelState, savedForm, nameof(Income), ItemIndex, nameof(Income.IncomeType), Form.Income[ItemIndex].IncomeType);
                    break;
                case 1:
                    AssignAndValidate(ModelState, savedForm, nameof(Income), ItemIndex, nameof(Income.Amount), Form.Income[ItemIndex].Amount);
                    break;
                case 2:
                    AssignAndValidate(ModelState, savedForm, nameof(Income), ItemIndex, nameof(Income.IncomeFrequency), Form.Income[ItemIndex].IncomeFrequency);
                    break;
                case 3:
                    AssignAndValidate(ModelState, savedForm, nameof(Income), ItemIndex, nameof(Income.Provider), Form.Income[ItemIndex].Provider);
                    return IfValidShowList(savedForm, "./IndividualsIncomeList");
            }

            return await IfValidNextPage(savedForm);
        }


        /// <summary>
        /// Ensures that the <see cref="Form.Income"/> collection is initialized and contains at least the number of
        /// entries required to include the item at the current <see cref="ItemIndex"/>.
        /// </summary>
        /// <remarks>If the <see cref="Form.Income"/> collection is empty or does not have enough entries
        /// to include the item at <see cref="ItemIndex"/>, new <see cref="Income"/> instances are added to the
        /// collection until the required size is reached. The updated form is then saved to the session.</remarks>
        private void EnsureIncomeEntryExists()
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
        }
    }
}
