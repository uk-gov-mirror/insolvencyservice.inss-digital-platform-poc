using INSS.Web.Components.Models;
using INSS.Web.Components.Services;

namespace INSS.Web.Components.Controllers;

public class SummaryController : BaseController<SectionModel>
{
    public SummaryController(IModelService<SectionModel> sectionService) : base(sectionService)
    {          
    }
}