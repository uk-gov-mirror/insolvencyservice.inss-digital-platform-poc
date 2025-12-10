using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    /// <summary>
    /// Controller responsible for handling company details form workflow.
    /// Manages a multi-step form process for collecting company information including
    /// company name, type (charity/limited), and specific details based on company type.
    /// </summary>
    [Authorize]
    public class CompanyDetailsController : Controller
    {
        /// <summary>
        /// Session key used for persisting company details data across form steps.
        /// </summary>
        private const string SessionKey = "CompanyDetails";

        /// <summary>
        /// Displays the company name form and resets any existing session data.
        /// This is the entry point for the company details workflow.
        /// </summary>
        /// <returns>The company name view.</returns>
        public IActionResult CompanyName()
        {
            ModelPersistence.Reset(HttpContext.Session, SessionKey);
            return View();
        }

        /// <summary>
        /// Processes the company name form submission.
        /// Validates the model and stores it in session before redirecting to the next step.
        /// </summary>
        /// <param name="model">The company details view model containing the company name.</param>
        /// <returns>Redirects to CharityOrLimited action if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult CompanyName(CompanyDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("CharityOrLimited");
        }

        /// <summary>
        /// Displays the charity or limited company selection form.
        /// Retrieves the current model state from session.
        /// </summary>
        /// <returns>The charity or limited selection view with the current model data.</returns>
        public IActionResult CharityOrLimited()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the charity or limited company selection form submission.
        /// Routes to appropriate next step based on the selected company type.
        /// </summary>
        /// <param name="model">The company details view model containing the company type selection.</param>
        /// <returns>
        /// Redirects to Charity action if charity is selected,
        /// Limited action if limited company is selected,
        /// or Completed action for other selections.
        /// Returns the view with validation errors if model is invalid.
        /// </returns>
        [HttpPost]
        public IActionResult CharityOrLimited(CompanyDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);

            if (model.IsCharityOrLtd == "charity")
            {
                return RedirectToAction("Charity");
            }
            else if (model.IsCharityOrLtd == "limited")
            {
                return RedirectToAction("Limited");
            }
            else
            {
                return RedirectToAction("Completed");
            }
        }

        /// <summary>
        /// Displays the charity-specific details form.
        /// Retrieves the current model state from session.
        /// </summary>
        /// <returns>The charity details view with the current model data.</returns>
        public IActionResult Charity()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the charity details form submission.
        /// Validates and stores the charity-specific information before completing the workflow.
        /// </summary>
        /// <param name="model">The company details view model containing charity-specific information.</param>
        /// <returns>Redirects to Completed action if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult Charity(CompanyDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("Completed");
        }

        /// <summary>
        /// Displays the limited company-specific details form.
        /// Retrieves the current model state from session.
        /// </summary>
        /// <returns>The limited company details view with the current model data.</returns>
        public IActionResult Limited()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the limited company details form submission.
        /// Validates and stores the limited company-specific information before completing the workflow.
        /// </summary>
        /// <param name="model">The company details view model containing limited company-specific information.</param>
        /// <returns>Redirects to Completed action if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult Limited(CompanyDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("Completed");
        }

        /// <summary>
        /// Displays the completion page showing the final collected company details.
        /// This is the final step in the company details workflow.
        /// </summary>
        /// <returns>The completion view with the final model data from session.</returns>
        public IActionResult Completed()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }
    }
}
