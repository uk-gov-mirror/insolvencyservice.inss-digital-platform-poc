using INSS.Web.Components.Models;
using INSS.Web.Components.Services;

namespace INSS.Web.Components.Controllers;

public class FullNameController : BaseController<FullNameModel>
{
    public FullNameController(IModelService<FullNameModel> fullNameService) : base(fullNameService)
    {          
    }
}