using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class FormService : IModelService<FormModel>
{
    private readonly IFormModelFactory _formModelFactory;
    private readonly IFormStateService _formStateService;

    public FormService(IFormModelFactory formModelFactory, IFormStateService  formStateService)
    {
        _formModelFactory = formModelFactory;
        _formStateService = formStateService;
    }
    
    public async Task<FormModel> LoadAsync(string? id)
    {
        var form = await _formModelFactory.CreateAsync();
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        return  form;
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