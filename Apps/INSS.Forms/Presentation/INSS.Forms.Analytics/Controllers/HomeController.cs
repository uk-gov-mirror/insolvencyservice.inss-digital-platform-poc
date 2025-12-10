using System.Diagnostics;
using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    /// <summary>
    /// Main controller for handling home page functionality, contact forms, and issue reporting.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Session key for storing contact us form data.
        /// </summary>
        private const string ContactUsSessionKey = "ContactUs";
        
        /// <summary>
        /// Session key for storing report issue form data.
        /// </summary>
        private const string ReportIssueSessionKey = "ReportIssue";

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The privacy policy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the contact us form and resets any existing session data.
        /// </summary>
        /// <returns>The contact us form view.</returns>
        public IActionResult ContactUs()
        {
            ModelPersistence.Reset(HttpContext.Session, ContactUsSessionKey);
            return View();
        }

        /// <summary>
        /// Handles the submission of the contact us form.
        /// </summary>
        /// <param name="model">The contact us form data submitted by the user.</param>
        /// <returns>
        /// If the model is valid, redirects to the contact us completed page.
        /// Otherwise, returns the contact us form view with validation errors.
        /// </returns>
        [HttpPost]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelPersistence.SetToSession(HttpContext.Session, ContactUsSessionKey, model);
                return RedirectToAction("ContactUsCompleted");
            }

            return View(model);
        }

        /// <summary>
        /// Displays the contact us completion page with the submitted form data.
        /// </summary>
        /// <returns>The contact us completed view with the form data from session.</returns>
        public IActionResult ContactUsCompleted()
        {
            var model = ModelPersistence.GetFromSession<ContactUsViewModel>(HttpContext.Session, ContactUsSessionKey);
            return View(model);
        }

        /// <summary>
        /// Displays the report issue form and resets any existing session data.
        /// </summary>
        /// <returns>The report issue form view.</returns>
        public IActionResult ReportIssue()
        {
            ModelPersistence.Reset(HttpContext.Session, ReportIssueSessionKey);
            return View();
        }

        /// <summary>
        /// Handles the submission of the report issue form.
        /// </summary>
        /// <param name="model">The report issue form data submitted by the user.</param>
        /// <returns>
        /// If the model is valid, redirects to the report issue completed page.
        /// Otherwise, returns the report issue form view with validation errors.
        /// </returns>
        [HttpPost]
        public IActionResult ReportIssue(ReportIssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelPersistence.SetToSession(HttpContext.Session, ReportIssueSessionKey, model);
                return RedirectToAction("ReportIssueCompleted");
            }

            return View(model);
        }

        /// <summary>
        /// Displays the report issue completion page with the submitted form data.
        /// </summary>
        /// <returns>The report issue completed view with the form data from session.</returns>
        public IActionResult ReportIssueCompleted()
        {
            var model = ModelPersistence.GetFromSession<ReportIssueViewModel>(HttpContext.Session, ReportIssueSessionKey);
            return View(model);
        }

        /// <summary>
        /// Displays the site map page.
        /// </summary>
        /// <returns>The site map view.</returns>
        public IActionResult SiteMap()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page with diagnostic information.
        /// </summary>
        /// <returns>The error view with request tracking information.</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
