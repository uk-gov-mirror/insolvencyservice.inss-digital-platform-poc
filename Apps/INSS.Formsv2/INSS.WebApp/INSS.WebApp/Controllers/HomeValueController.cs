using INSS.Web.Components.Controllers;
using INSS.Web.Components.Services;
using INSS.WebApp.Models;

namespace INSS.WebApp.Controllers;

public class HomeValueController : BaseController<HomeValueModel>
{
    public HomeValueController(IModelService<HomeValueModel> modelService) : base(modelService)
    {
    }
}