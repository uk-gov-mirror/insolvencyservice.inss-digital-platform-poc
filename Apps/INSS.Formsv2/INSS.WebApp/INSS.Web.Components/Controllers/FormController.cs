using INSS.Web.Components.Models;
using INSS.Web.Components.Services;

namespace INSS.Web.Components.Controllers;

public class FormController : BaseController<FormModel>
{
    public FormController(IModelService<FormModel> modelService) : base(modelService)
    {
    }
}