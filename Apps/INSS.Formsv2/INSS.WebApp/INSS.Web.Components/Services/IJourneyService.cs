using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public interface IJourneyService
{
    void TransitionPrevious(FormModel form, PageModel? pageModel = null);

    void TransitionNext(FormModel form, PageModel pageModel);
}