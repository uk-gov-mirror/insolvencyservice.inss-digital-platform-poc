using INSS.Platform.Portal.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Platform.Portal.Application.Clients;

public interface IBankAccountClient
{
    Task ValidateBankDetailsAsync(ModelStateDictionary modelState, BankAccountModel model);
}