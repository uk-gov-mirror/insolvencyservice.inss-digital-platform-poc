using INSS.Platform.Portal.Application.Clients;
using INSS.Platform.Portal.Application.Resolvers;
using INSS.Platform.Portal.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Platform.Portal.Application.Services;

public class BankAccountService : BasePageModelService<BankAccountModel>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IBankAccountClient _bankAccountClient;

    public BankAccountService(
        IHttpClientFactory clientFactory, 
        IFormStateService  formStateService, 
        IJourneyService  journeyService,
        IUserSessionResolver userSessionResolver,
        IBankAccountClient bankAccountClient)
        : base(formStateService, journeyService, userSessionResolver)
    {
        _clientFactory = clientFactory;
        _bankAccountClient = bankAccountClient;
    }
    
    protected override async Task ValidateAdditionalAsync(ModelStateDictionary modelState, BankAccountModel model)
    {
        await _bankAccountClient.ValidateBankDetailsAsync(modelState, model);
    }

    protected override void CopySourceToTargetModel(BankAccountModel sourceModel, BankAccountModel targetModel)
    {
        targetModel.AccountNumber = sourceModel.AccountNumber;
        targetModel.SortCode = sourceModel.SortCode;
    }
}