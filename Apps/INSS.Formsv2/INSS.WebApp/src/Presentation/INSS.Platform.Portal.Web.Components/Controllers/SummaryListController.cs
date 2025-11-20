using INSS.Platform.Portal.Application.Services;
using INSS.Platform.Portal.Domain;

namespace INSS.Platform.Portal.Web.Components.Controllers;

public class SummaryListController : BaseController<SummaryListModel>
{
    private readonly IModelService<SummaryListModel> _modelService;

    public SummaryListController(IModelService<SummaryListModel> modelService) : base(modelService)
    {
        _modelService = modelService;
    }
}