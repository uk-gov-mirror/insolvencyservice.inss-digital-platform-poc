using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public sealed class SectionService : IModelService<SectionModel>
{
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public SectionService(IFormStateService formStateService, IJourneyService  journeyService)
    {
        _formStateService = formStateService;
        _journeyService = journeyService;
    }
    
    public async Task<SectionModel> LoadAsync(string? id)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");
        var section = form.FindSection(id!);
        //_journeyService.TransitionPrevious(form, page);
        return section;
    }

    public Task ValidateAsync(ModelStateDictionary modelState, SectionModel model)
    {
        throw new NotImplementedException();
    }

    public Task<string> SaveAsync(SectionModel model)
    {
        throw new NotImplementedException();
    }
}