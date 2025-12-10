using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    /// <summary>
    /// Controller responsible for managing the multi-step director registration process.
    /// Handles the collection of director information, company details, shareholding information,
    /// and maintains state across multiple steps using session storage.
    /// </summary>
    [Authorize]
    public class RegisterDirectorController : Controller
    {
        /// <summary>
        /// Session key used for persisting the registration model across multiple requests.
        /// </summary>
        private const string SessionKey = "RegisterDirector";

        /// <summary>
        /// Displays the initial step for collecting director name information.
        /// Resets any existing session data to ensure a clean start.
        /// </summary>
        /// <returns>The DirectorName view.</returns>
        public IActionResult DirectorName()
        {
            ModelPersistence.Reset(HttpContext.Session, SessionKey);
            return View();
        }

        /// <summary>
        /// Processes the director name form submission and advances to the next step.
        /// </summary>
        /// <param name="model">The registration model containing director name information.</param>
        /// <returns>Redirects to CompanyName on success, or returns the current view with validation errors.</returns>
        [HttpPost]
        public IActionResult DirectorName(RegisterDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("CompanyName");
        }

        /// <summary>
        /// Displays the company name collection step.
        /// Retrieves the current registration model from session storage.
        /// </summary>
        /// <returns>The CompanyName view with the current registration model.</returns>
        public IActionResult CompanyName()
        {
            var model = ModelPersistence.GetFromSession<RegisterDirectorViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the company name form submission and advances to the next step.
        /// </summary>
        /// <param name="model">The registration model containing company name information.</param>
        /// <returns>Redirects to MajorityShareholder on success, or returns the current view with validation errors.</returns>
        [HttpPost]
        public IActionResult CompanyName(RegisterDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("MajorityShareholder");
        }

        /// <summary>
        /// Displays the majority shareholder information collection step.
        /// Retrieves the current registration model from session storage.
        /// </summary>
        /// <returns>The MajorityShareholder view with the current registration model.</returns>
        public IActionResult MajorityShareholder()
        {
            var model = ModelPersistence.GetFromSession<RegisterDirectorViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the majority shareholder form submission and advances to the next step.
        /// </summary>
        /// <param name="model">The registration model containing majority shareholder information.</param>
        /// <returns>Redirects to SharePercentage on success, or returns the current view with validation errors.</returns>
        [HttpPost]
        public IActionResult MajorityShareholder(RegisterDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("SharePercentage");
        }

        /// <summary>
        /// Displays the share percentage collection step.
        /// Retrieves the current registration model from session storage.
        /// </summary>
        /// <returns>The SharePercentage view with the current registration model.</returns>
        public IActionResult SharePercentage()
        {
            var model = ModelPersistence.GetFromSession<RegisterDirectorViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the share percentage form submission and advances to the completion step.
        /// </summary>
        /// <param name="model">The registration model containing share percentage information.</param>
        /// <returns>Redirects to Completed on success, or returns the current view with validation errors.</returns>
        [HttpPost]
        public IActionResult SharePercentage(RegisterDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);
            return RedirectToAction("Completed");
        }

        /// <summary>
        /// Displays the registration completion page with all collected information.
        /// Retrieves the final registration model from session storage for review.
        /// </summary>
        /// <returns>The Completed view with the final registration model.</returns>
        public IActionResult Completed()
        {
            var model = ModelPersistence.GetFromSession<RegisterDirectorViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }
    }
}
