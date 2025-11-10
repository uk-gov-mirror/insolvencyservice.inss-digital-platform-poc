using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public sealed class DefaultJourneyResolver : IJourneyResolver
{
    public PageModel? Resolve(FormModel form, PageModel pageModel)
    {
        var section = form.FindSectionForPage(pageModel.PageUrl);
        return section.GetNextPage(pageModel.PageUrl);
    }
}