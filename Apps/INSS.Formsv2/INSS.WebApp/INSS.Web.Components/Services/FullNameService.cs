using INSS.Web.Components.Models;
using INSS.Web.Components.Resolvers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public class FullNameService : BasePageModelService<FullNameModel>
{
    private readonly IHttpClientFactory _clientFactory;

    public FullNameService(
        IHttpClientFactory clientFactory, 
        IFormStateService  formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver userSessionResolver)
        : base(formStateService, journeyService, userSessionResolver)
    {
        _clientFactory = clientFactory;
    }
    
    protected override async Task ValidateAdditionalAsync(ModelStateDictionary modelState, FullNameModel model)
    {
        // Any additional validation logic for FullNameModel can be added here if needed.
    }

    protected override void CopySourceToTargetModel(FullNameModel sourceModel, FullNameModel targetModel)
    {
        targetModel.FullName = sourceModel.FullName;
    }
}