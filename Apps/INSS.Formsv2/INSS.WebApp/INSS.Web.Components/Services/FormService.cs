using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class FormService : IModelService<FormModel>
{
    private readonly IFormModelFactory _formModelFactory;

    public FormService(IFormModelFactory formModelFactory)
    {
        _formModelFactory = formModelFactory;
    }
    
    public async Task<FormModel> LoadAsync()
    {
        // TODO: Create model from factory and save to session for user
        return await _formModelFactory.CreateAsync();
    }

    public Task ValidateAsync(ModelStateDictionary modelState, FormModel model)
    {
        return Task.CompletedTask;
    }

    public Task SaveAsync(FormModel model)
    {
        return Task.CompletedTask;
    }
}