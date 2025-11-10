using INSS.Web.Components.Models;
using INSS.Web.Components.Resolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class BankAccountService : IModelService<BankAccountModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;
    private readonly IUserSessionResolver _userSessionResolver;

    public BankAccountService(
        IHttpClientFactory clientFactory, 
        IFormStateService  formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver userSessionResolver)
    {
        _clientFactory = clientFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
        _userSessionResolver = userSessionResolver;
    }

    public async Task<BankAccountModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<BankAccountModel>(pageUrl!);
        _journeyService.TransitionPrevious(form, page);
        return page;
    }

    public async Task ValidateAsync(ModelStateDictionary modelState, BankAccountModel model)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        
        if (!modelState.IsValid)
        {
            model.PreviousPageUrl = form.NavigationHistory.Last();
            return;
        }

        var client = _clientFactory.CreateClient();
        client.BaseAddress = new Uri("https://vseries.bottomline.com/api/");

        var response = await client.GetAsync($"getukbankbranch/?apikey=2T2-2E42AEF5-3CF8-4FD9-B1A5-09A9BF03551D&sortCode={model.SortCode}");

        if (!response.IsSuccessStatusCode || await response.Content.ReadAsStringAsync() == "null")
        {
            model.PreviousPageUrl = form.NavigationHistory.Last();
            modelState.AddModelError(nameof(model.SortCode), "Bank account sort code not found");
        }
    }

    public async Task<string> SaveAsync(string requestPath, BankAccountModel model)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        var page = form.FindPage<BankAccountModel>(requestPath);
        form.AddNavigation(page.PageUrl);
        
        page.AccountNumber = model.AccountNumber;
        page.SortCode = model.SortCode;
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        
        _journeyService.TransitionNext(form, page);
        return page.NextPageUrl;
    }
}