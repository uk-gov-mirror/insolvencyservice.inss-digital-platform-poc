using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    /// <summary>
    /// Controller responsible for managing the "About You" form flow, including collecting user's title, first name, and last name information.
    /// </summary>
    [Authorize]
    public class AboutYouController : Controller
    {
        /// <summary>
        /// Session key used to store the AboutYou form data across requests.
        /// </summary>
        private const string SessionKey = "AboutYou";

        /// <summary>
        /// Displays the title selection page and resets any existing session data.
        /// </summary>
        /// <returns>The title selection view.</returns>
        public IActionResult Title()
        {
            ModelPersistence.Reset(HttpContext.Session, SessionKey);
            return View();
        }

        /// <summary>
        /// Processes the submitted title form data.
        /// </summary>
        /// <param name="model">The AboutYou view model containing the user's title selection.</param>
        /// <returns>Redirects to FirstName page if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult Title(AboutYouViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);

            return RedirectToAction("FirstName"); 
        }

        /// <summary>
        /// Displays the first name input page, retrieving any previously entered data from session.
        /// </summary>
        /// <returns>The first name input view with the current model state.</returns>
        public IActionResult FirstName()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the submitted first name form data.
        /// </summary>
        /// <param name="model">The AboutYou view model containing the user's first name.</param>
        /// <returns>Redirects to LastName page if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult FirstName(AboutYouViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);

            return RedirectToAction("LastName");
        }

        /// <summary>
        /// Displays the last name input page, retrieving any previously entered data from session.
        /// </summary>
        /// <returns>The last name input view with the current model state.</returns>
        public IActionResult LastName()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

        /// <summary>
        /// Processes the submitted last name form data.
        /// </summary>
        /// <param name="model">The AboutYou view model containing the user's last name.</param>
        /// <returns>Redirects to Completed page if valid, otherwise returns the view with validation errors.</returns>
        [HttpPost]
        public IActionResult LastName(AboutYouViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelPersistence.SetToSession(HttpContext.Session, SessionKey, model);

            return RedirectToAction("Completed");
        }

        /// <summary>
        /// Displays the completion page showing all collected user information.
        /// </summary>
        /// <returns>The completion view with the complete AboutYou model data.</returns>
        public IActionResult Completed()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }
    }
}
