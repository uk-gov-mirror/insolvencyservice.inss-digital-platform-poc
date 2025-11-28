using INSS.Forms.Analytics.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Analytics.Controllers
{
    public class AboutYouController : Controller
    {
        private const string SessionKey = "AboutYou";

        public IActionResult Title()
        {
            ModelPersistence.Reset(HttpContext.Session, SessionKey);
            return View();
        }

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

        public IActionResult FirstName()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

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

        public IActionResult LastName()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }

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

        public IActionResult Completed()
        {
            var model = ModelPersistence.GetFromSession<AboutYouViewModel>(HttpContext.Session, SessionKey);
            return View(model);
        }
    }
}
