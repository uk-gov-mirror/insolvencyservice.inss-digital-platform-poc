using INSS.Web.Components.Resolvers;
using INSS.Web.Components.Services;
using INSS.WebApp.Models;

namespace INSS.WebApp.Services;

public class HomeValueService : BasePageModelService<HomeValueModel>
{
    public HomeValueService(
        IFormStateService formStateService,
        IJourneyService  journeyService,
        IUserSessionResolver  userSessionResolver)
        : base(formStateService, journeyService, userSessionResolver)
    {
    }

    protected override void CopySourceToTargetModel(HomeValueModel sourceModel, HomeValueModel targetModel)
    {
        targetModel.Value = sourceModel.Value;
    }
}