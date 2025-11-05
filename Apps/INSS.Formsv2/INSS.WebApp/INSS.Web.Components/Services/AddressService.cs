using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class AddressService : IModelService<AddressModel>
{
    private readonly IHttpClientFactory _clientFactory;
 
    public AddressService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
 
    public async Task<AddressModel> LoadAsync()
    {
        await Task.Delay(100); // Simulate async work
        return new AddressModel();
    }
 
    public async Task ValidateAsync(ModelStateDictionary modelState, AddressModel model)
    {
        if (!modelState.IsValid)
        {
            return;
        }
        // Do some additonal validation if required
    }
 
    public async Task SaveAsync(AddressModel model)
    {
        await Task.Delay(100); // Simulate async work
        // Save logic here
    }
}