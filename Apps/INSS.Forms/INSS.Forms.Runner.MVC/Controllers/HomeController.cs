using INSS.Forms.Runner.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INSS.Forms.Runner.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeModel{ AccountNumber = "12345678" });
        }

        [HttpPost]
        public IActionResult Index(HomeModel model)
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
