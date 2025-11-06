using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public sealed class JourneyService : IJourneyService
{
    private readonly IServiceProvider _serviceProvider;

    public JourneyService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void TransitionNext(FormModel form, PageModel? pageModel = null)
    {
        if (pageModel is null)
        {
            // For the task list set all the section first questions to return to the task list
            foreach (var section in form.Sections)
            {
                section.Pages.First().Back = form.Path;
            }
        }
        else
        {
            var resolver = GetJourneyResolver(pageModel);
            
            var nextPage = resolver.Resolve(form, pageModel);

            if (nextPage is null)
            {
                // TODO: Probably go to the summary for the section. On summary the goto the form task list
                pageModel.Back = form.Path;
            }
            else
            {
                pageModel.Next = nextPage.Path;
                nextPage.Back = pageModel.Path;
            }
        }
    }

    private IJourneyResolver GetJourneyResolver(PageModel page)
    {
        var resolverType = typeof(IJourneyResolver<>).MakeGenericType(page.GetType());
        return (IJourneyResolver)(_serviceProvider.GetService(resolverType) ?? new DefaultJourneyResolver());
    }
}