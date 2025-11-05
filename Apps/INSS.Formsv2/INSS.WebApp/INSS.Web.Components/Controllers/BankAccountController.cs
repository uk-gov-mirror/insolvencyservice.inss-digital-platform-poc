using INSS.Web.Components.Models;
using INSS.Web.Components.Services;

namespace INSS.Web.Components.Controllers;

public class BankAccountController : BaseController<BankAccountModel>
{
    public BankAccountController(IModelService<BankAccountModel> bankAccountService) : base(bankAccountService)
    {          
    }
}