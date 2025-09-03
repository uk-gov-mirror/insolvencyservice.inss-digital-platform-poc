using INSS.Forms.Domain.Models.Composite;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class AddressViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Address model)
        {
            return View(model);
        }
    }
}


