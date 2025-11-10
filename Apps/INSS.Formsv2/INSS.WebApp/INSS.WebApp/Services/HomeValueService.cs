using INSS.Web.Components.Resolvers;
using INSS.Web.Components.Services;
using INSS.WebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.WebApp.Services;

public class HomeValueService : IModelService<HomeValueModel>
{
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;
    private readonly IUserSessionResolver _userSessionResolver;

    public HomeValueService(
        IFormStateService formStateService,
        IJourneyService  journeyService,
        IUserSessionResolver  userSessionResolver)
    {
        _formStateService = formStateService;
        _journeyService = journeyService;
        _userSessionResolver = userSessionResolver;
    }
    
    public async Task<HomeValueModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<HomeValueModel>(pageUrl!);
        _journeyService.TransitionPrevious(form, page);
        return page;
    }

    public async Task ValidateAsync(ModelStateDictionary modelState, HomeValueModel model)
    {
        if (!modelState.IsValid)
        {
            var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
            model.PreviousPageUrl = form.NavigationHistory.Last();
            return;
        }
        // Do some additional validation if required
    }

    public async Task<string> SaveAsync(string requestPath, HomeValueModel model)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<HomeValueModel>(requestPath);
        form.AddNavigation(page.PageUrl);
        
        page.Value = model.Value;
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        
        _journeyService.TransitionNext(form, page);
        
        return page.NextPageUrl;
    }
}