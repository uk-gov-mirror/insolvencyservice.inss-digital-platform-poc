using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class FormService : IModelService<FormModel>
{
    private readonly IFormModelFactory _formModelFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public FormService(
        IFormModelFactory formModelFactory, 
        IFormStateService  formStateService, 
        IJourneyService  journeyService)
    {
        _formModelFactory = formModelFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
    }
    
    public async Task<FormModel> LoadAsync(string? id)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");

        if (form is null)
        {
            form = await _formModelFactory.CreateAsync();
            form.Initialize();
        }

        form.PopAllNavigationHistory();
        form.AddNavigation(form.PageUrl);
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        return  form;
    }

    public Task ValidateAsync(ModelStateDictionary modelState, FormModel model)
    {
        return Task.CompletedTask;
    }

    public Task<string> SaveAsync(FormModel model)
    {
        return Task.FromResult(model.PageUrl);
    }
}