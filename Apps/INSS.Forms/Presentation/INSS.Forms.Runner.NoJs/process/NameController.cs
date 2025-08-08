using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Runner.NoJs.process
{
    public class NameController : Controller
    {
        [HttpPost]
        [Route("/process/name")]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string model)
        {
            // TODO: Save Model Here

            return Ok();
        }
    }
}
