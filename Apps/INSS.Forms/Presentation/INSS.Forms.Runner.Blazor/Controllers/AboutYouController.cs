using INSS.Forms.BCL.Services;
using INSS.Forms.Domain.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Forms.Runner.Blazor.Controllers
{
    [Route("about-you")]
    public class AboutYouController : BaseController<AboutYou>
    {

        public AboutYouController(IConfiguration configuration, IFormApiClient formApiClient, IFormMetadataService formMetadataService):
            base(configuration, formApiClient, formMetadataService)
        {
        }

        [HttpPost("submit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(AboutYou model)
        {
            switch (PageIndex)
            {
                case 0:
                    return await Next(ModelState, nameof(Form.Name), Form.Name);
                case 1:
                    return await Next(ModelState, nameof(Form.Address), Form.Address);
                case 2:
                    return await Next(ModelState, nameof(Form.Telephone), Form.Telephone);
                case 3:
                    return await Next(ModelState, nameof(Form.Email), Form.Email, true);
                default:
                    throw new InvalidOperationException("Invalid page index");
            }
        }

        protected async Task<IActionResult> Next(ModelStateDictionary modelState, string property, object? value, bool saveToDatabase = false)
        {
            ModelStateHelpers.OnlyValidateProperty(modelState, property);
            if (!ModelState.IsValid)
            {
                TempData["ValidationErrors"] = string.Join(";", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Redirect("/about-you");
            }

            LoadFormFromSession();

            ModelStateHelpers.SetPropertyValueByName(Form, property, value);

            SaveFormToSession(Form);

            PageIndex++;

            if (saveToDatabase)
            {
                if (await SaveFormToDatabase())
                {
                    return Redirect(Form.FormMetadata.ReturnUrl);
                }
            }

            return Redirect("/about-you");
        }

    }
}

