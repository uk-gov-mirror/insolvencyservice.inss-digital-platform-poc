using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public interface IJourneyService
{
    void TransitionPrevious(FormModel form, PageModel? pageModel = null);

    void TransitionNext(FormModel form, PageModel pageModel);
}

public interface IJourneyResolver
{
    PageModel? Resolve(FormModel form, PageModel pageModel);
}

public interface IJourneyResolver<in TPageModel> : IJourneyResolver where TPageModel : PageModel
{
}

public sealed class DefaultJourneyResolver : IJourneyResolver //<BaseQuestionModel>
{
    public PageModel? Resolve(FormModel form, PageModel pageModel)
    {
        var section = form.FindSectionForPage(pageModel.Id);
        return section.GetNextPage(pageModel.Id);
    }
}