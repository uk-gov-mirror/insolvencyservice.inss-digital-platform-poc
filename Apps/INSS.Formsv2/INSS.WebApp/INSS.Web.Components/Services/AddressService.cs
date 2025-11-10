using INSS.Web.Components.Models;
using INSS.Web.Components.Resolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class AddressService : IModelService<AddressModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;
    private readonly IUserSessionResolver _userSessionResolver;

    public AddressService(
        IHttpClientFactory clientFactory, 
        IFormStateService formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver  userSessionResolver)
    {
        _clientFactory = clientFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
        _userSessionResolver = userSessionResolver;
    }
 
    public async Task<AddressModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<AddressModel>(pageUrl!);
        _journeyService.TransitionPrevious(form, page);
        return page;
    }
 
    public async Task ValidateAsync(ModelStateDictionary modelState, AddressModel model)
    {
        if (!modelState.IsValid)
        {
            var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
            model.PreviousPageUrl = form.NavigationHistory.Last();
            return;
        }
        // Do some additional validation if required
    }
 
    public async Task<string> SaveAsync(string requestPath, AddressModel model)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<AddressModel>(requestPath);
        form.AddNavigation(page.PageUrl);
        
        page.AddressLine1 = model.AddressLine1;
        page.AddressLine2 = model.AddressLine2;
        page.TownCity = model.TownCity;
        page.County = model.County;
        page.Postcode = model.Postcode;
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        
        _journeyService.TransitionNext(form, page);
        
        return page.NextPageUrl;
    }
}