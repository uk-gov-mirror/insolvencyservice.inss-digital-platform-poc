using INSS.Platform.Portal.Application.Clients;
using INSS.Platform.Portal.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Platform.Portal.Infrastructure;

public class BankAccountClient : IBankAccountClient
{
    private readonly HttpClient _client;

    public BankAccountClient(HttpClient client)
    {
        _client = client;
    }

    public async Task ValidateBankDetailsAsync(
        ModelStateDictionary modelState, 
        BankAccountModel model)
    {
        const string accountNotValid = "null";

        // TODO: This is hardcoded but we will use an alternative provider
        var response = await _client.GetAsync($"getukbankbranch/?apikey=2T2-2E42AEF5-3CF8-4FD9-B1A5-09A9BF03551D&sortCode={model.SortCode}");

        if (!response.IsSuccessStatusCode || 
            await response.Content.ReadAsStringAsync() == accountNotValid)
        {
            modelState.AddModelError(nameof(model.SortCode), "Bank account sort code not found");
        }
    }
}