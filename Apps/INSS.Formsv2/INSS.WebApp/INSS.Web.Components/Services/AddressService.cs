using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class AddressService : IModelService<AddressModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public AddressService(
        IHttpClientFactory clientFactory, 
        IFormStateService formStateService, 
        IJourneyService  journeyService)
    {
        _clientFactory = clientFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
    }
 
    public async Task<AddressModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var page = form.FindPage<AddressModel>(pageUrl!);
        _journeyService.TransitionPrevious(form, page);
        return page;
    }
 
    public async Task ValidateAsync(ModelStateDictionary modelState, AddressModel model)
    {
        if (!modelState.IsValid)
        {
            var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
            model.PreviousPageUrl = form.NavigationHistory.Last();
            return;
        }
        // Do some additional validation if required
    }
 
    public async Task<string> SaveAsync(string requestPath, AddressModel model)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var page = form.FindPage<AddressModel>(requestPath);
        form.AddNavigation(page.PageUrl);
        
        page.AddressLine1 = model.AddressLine1;
        page.AddressLine2 = model.AddressLine2;
        page.TownCity = model.TownCity;
        page.County = model.County;
        page.Postcode = model.Postcode;
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        
        _journeyService.TransitionNext(form, page);
        
        return page.NextPageUrl;
    }
}