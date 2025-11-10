using INSS.Web.Components.Models;

namespace INSS.Web.Components.Resolvers;

public interface IJourneyResolver<in TPageModel> : IJourneyResolver where TPageModel : PageModel
{
}

public interface IJourneyResolver
{
    PageModel? Resolve(FormModel form, PageModel pageModel);
}