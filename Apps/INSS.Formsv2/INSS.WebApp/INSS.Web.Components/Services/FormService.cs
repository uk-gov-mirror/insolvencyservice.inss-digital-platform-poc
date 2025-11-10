using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using INSS.Web.Components.Resolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class FormService : IModelService<FormModel>
{
    private readonly IFormModelFactory _formModelFactory;
    private readonly IFormStateService _formStateService;
    private readonly IUserSessionResolver _userSessionResolver;
    
    public FormService(
        IFormModelFactory formModelFactory, 
        IFormStateService  formStateService,
        IUserSessionResolver userSessionResolver)
    {
        _formModelFactory = formModelFactory;
        _formStateService = formStateService;
        _userSessionResolver = userSessionResolver;
    }
    
    public async Task<FormModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());

        if (form is null)
        {
            form = await _formModelFactory.CreateAsync();
        }

        form.PopAllNavigationHistory();
        form.AddNavigation(form.PageUrl);
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        return  form;
    }

    public Task ValidateAsync(ModelStateDictionary modelState, FormModel model)
    {
        return Task.CompletedTask;
    }

    public async Task<string> SaveAsync(string requestPath, FormModel model)
    {
        var form = await _formStateService.GetAsync(_userSessionResolver.GetUserId());
        form.PopAllNavigationHistory();
        await _formStateService.SaveAsync(_userSessionResolver.GetUserId(), form);
        
        // TODO: Push to an API
        
        return await Task.FromResult(requestPath);
    }
}