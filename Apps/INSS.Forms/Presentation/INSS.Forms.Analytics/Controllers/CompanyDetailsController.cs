using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    public class CompanyDetailsController : Controller
    {
        private const string SessionKey = "CompanyDetails";

        public IActionResult CompanyName()
        {
            ModelPersistence.Reset(HttpContext.Session, SessionKey);
            return View();
        }

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

        public IActionResult CharityOrLimited()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

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

        public IActionResult Charity()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

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

        public IActionResult Limited()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

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

        public IActionResult Completed()
        {
            var model = ModelPersistence.GetFromSession<CompanyDetailsViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }
    }
}
