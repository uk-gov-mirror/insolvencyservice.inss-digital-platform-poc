using INSS.Web.Components.Services;
using INSS.WebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.WebApp.Services;

public class HomeValueService : IModelService<HomeValueModel>
{
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public HomeValueService(IFormStateService formStateService, IJourneyService  journeyService)
    {
        _formStateService = formStateService;
        _journeyService = journeyService;
    }
    
    public async Task<HomeValueModel> LoadAsync(string? id)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var page = form.FindPage<HomeValueModel>(id!);
        _journeyService.TransitionPrevious(form, page);
        return page;
    }

    public async Task ValidateAsync(ModelStateDictionary modelState, HomeValueModel model)
    {
        if (!modelState.IsValid)
        {
            var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
            model.Previous = form.NavigationHistory.Last();
            return;
        }
        // Do some additional validation if required
    }

    public async Task<string> SaveAsync(HomeValueModel model)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var page = form.FindPage<HomeValueModel>(model.Id);
        var section = form.FindSectionForPage(page.Id);
        page.Path.PageUrl = section.GetPageUrl(form, page);
        form.AddNavigation(page.Path);
        
        page.Value = model.Value;
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        
        _journeyService.TransitionNext(form, page);
        
        return page.Next.PageUrl;
    }
}