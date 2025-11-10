using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class SectionService : IModelService<SectionModel>
{
    private readonly IFormStateService _formStateService;

    public SectionService(IFormStateService formStateService)
    {
        _formStateService = formStateService;
    }
    
    public async Task<SectionModel> LoadAsync(string? pageUrl)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var section = form.FindSection(pageUrl!);
        return section;
    }

    public Task ValidateAsync(ModelStateDictionary modelState, SectionModel model)
    {
        return Task.CompletedTask;
    }

    public async Task<string> SaveAsync(string requestPath, SectionModel model)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var section = form.FindSection(requestPath);
        section.IsComplete = true;
        await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
        return form.PageUrl;
    }
}