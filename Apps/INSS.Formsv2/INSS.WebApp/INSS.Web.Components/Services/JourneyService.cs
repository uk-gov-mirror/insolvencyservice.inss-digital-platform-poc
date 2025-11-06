using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public sealed class JourneyService : IJourneyService
{
    private readonly IServiceProvider _serviceProvider;
    private string? _currentPageId;

    public JourneyService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Navigation TransitionNext(FormModel form, PageModel? pageModel = null)
    {
        PageModel? nextPage;
        
        if (_currentPageId is null && pageModel is null)
        {
            foreach (var section in form.Sections)
            {
                foreach (var page in section.Pages)
                {
                    page.Question.Back = form.Path;
                }
            }
            
            //nextPage = form.Sections.First().Pages.First();
        }
        else if (pageModel is not null) // _currentPageId is null && 
        {
            //var currentPage = form.FindPage(_currentPageId);

            var resolver = GetJourneyResolver(pageModel.Question);
            
            nextPage = resolver.Resolve(form, pageModel.Question);

            if (nextPage is null)
            {
                // TODO: Probably go to the summary for the section. On summary the goto the form task list
                _currentPageId = null;
                return form.Path;
            }

            nextPage.Question.Back = pageModel.Path;
            
            //_currentPageId = nextPage.Id;
            return nextPage.Path;
        }

        return form.Path;
    }

    private IJourneyResolver GetJourneyResolver(BaseQuestionModel question)
    {
        var resolverType = typeof(IJourneyResolver<>).MakeGenericType(question.GetType());
        return (IJourneyResolver)(_serviceProvider.GetService(resolverType) ?? new DefaultJourneyResolver());
    }
}