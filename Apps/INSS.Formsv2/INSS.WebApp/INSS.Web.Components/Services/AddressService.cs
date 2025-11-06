using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace INSS.Web.Components.Services;

public class AddressService : IModelService<AddressModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IFormStateService _formStateService;
    private readonly IJourneyService _journeyService;

    public AddressService(
        IHttpClientFactory clientFactory, 
        IFormStateService formStateService, 
        IJourneyService  journeyService)
    {
        _clientFactory = clientFactory;
        _formStateService = formStateService;
        _journeyService = journeyService;
    }
 
    public async Task<AddressModel> LoadAsync(string? id)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");

       foreach (var section in form.Sections)
        {
            foreach (var page in section.Pages)
            {
                if (page.Question.Id == id && page.Question is AddressModel addressModel)
                {
                    addressModel.Back = _journeyService.TransitionNext(form, page);
                    return addressModel;
                }
            }
        }

        throw new Exception("Unable to find question from Id"); // TODO: Better method!
    }
 
    public async Task ValidateAsync(ModelStateDictionary modelState, AddressModel model)
    {
        if (!modelState.IsValid)
        {
            return;
        }
        // Do some additonal validation if required
    }
 
    public async Task<Navigation> SaveAsync(AddressModel model)
    {
        var form = await _formStateService.GetAsync("0c4d0123-854b-4929-8a75-6b89c6619909");

        
        // TODO: Resolve which address (or model) as it might appear multiple times!
        
        /*foreach (var section in form.Sections)
        {
            foreach (var page in section.Pages)
            {
                var question = page.Questions.FirstOrDefault(q => q is AddressModel);

                if (question is AddressModel addressModel)
                {
                    addressModel.AddressLine1 = model.AddressLine1;
                    addressModel.AddressLine2 = model.AddressLine2;
                    addressModel.TownCity = model.TownCity;
                    addressModel.County = model.County;
                    addressModel.Postcode = model.Postcode;
                    await _formStateService.SaveAsync("0c4d0123-854b-4929-8a75-6b89c6619909", form);
                }
            }
        }*/

        return _journeyService.TransitionNext(form);
    }
}