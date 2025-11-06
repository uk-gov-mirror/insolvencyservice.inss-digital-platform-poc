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
        var form = await _formModelFactory.CreateAsync();
        form.PopAllNavigationHistory();
        form.AddNavigation(form.Path);
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        return  form;
    }

    public Task ValidateAsync(ModelStateDictionary modelState, FormModel model)
    {
        return Task.CompletedTask;
    }

    public Task<Navigation> SaveAsync(FormModel model)
    {
        // TODO: Do we need to get it?
        //var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        //_journeyService.TransitionNext(form);
        return Task.FromResult(new Navigation { Controller = "Form", Id = model.Id });
    }
}