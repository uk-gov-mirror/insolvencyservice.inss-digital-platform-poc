using INSS.Forms.Runner.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Forms.Runner.MVC.Services
{
    public class BankAccountService : IModelService<BankAccountModel>
    {
        private readonly IHttpClientFactory _clientFactory;

        public BankAccountService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<BankAccountModel> LoadAsync()
        {
            await Task.Delay(100); // Simulate async work
            return new BankAccountModel();
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

        public async Task SaveAsync(BankAccountModel model)
        {
            await Task.Delay(100); // Simulate async work
            // Save logic here
        }
    }
}
