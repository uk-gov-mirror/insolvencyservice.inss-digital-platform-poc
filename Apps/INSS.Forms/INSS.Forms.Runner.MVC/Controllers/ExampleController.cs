using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Runner.MVC.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
