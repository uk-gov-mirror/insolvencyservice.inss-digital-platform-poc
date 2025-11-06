using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class BankAccountService : IModelService<BankAccountModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public BankAccountService(
        IHttpClientFactory clientFactory, 
        IFormStateService  formStateService, 
        IJourneyService  journeyService)
    {
        _clientFactory = clientFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
    }

    public async Task<BankAccountModel> LoadAsync(string? id)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");

        //_journeyService.TransitionNext(form);
        
        foreach (var section in form.Sections)
        {
            foreach (var page in section.Pages)
            {
                if (page.Question.Id == id && page.Question is BankAccountModel bankAccountModel)
                {
                    bankAccountModel.Back = _journeyService.TransitionNext(form, page);
                    return bankAccountModel;
                }
            }
        }

        throw new Exception("Unable to find question from Id"); // TODO: Better method!
    }

    public async Task ValidateAsync(ModelStateDictionary modelState, BankAccountModel model)
    {
        if(!modelState.IsValid)
        {
            return;
        }

        var client = _clientFactory.CreateClient();
        client.BaseAddress = new Uri("https://vseries.bottomline.com/api/");

        var response = await client.GetAsync($"getukbankbranch/?apikey=2T2-2E42AEF5-3CF8-4FD9-B1A5-09A9BF03551D&sortCode={model.SortCode}");

        if (!response.IsSuccessStatusCode || await response.Content.ReadAsStringAsync() == "null")
        {
            modelState.AddModelError(nameof(model.SortCode), "Bank account sort code not found");
        }
    }

    public async Task<Navigation> SaveAsync(BankAccountModel model)
    {
        // TODO: Save bank account
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        return _journeyService.TransitionNext(form);
    }
}