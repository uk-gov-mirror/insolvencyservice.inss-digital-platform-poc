using INSS.Forms.Runner.MVC.Models;
using INSS.Forms.Runner.MVC.Services;
using System.Diagnostics;

namespace INSS.Forms.Runner.MVC.Controllers
{

    public class BankAccountController : BaseController<BankAccountModel>
    {
        public BankAccountController(IModelService<BankAccountModel> bankAccountService) : base(bankAccountService)
        {          
        }
    }
}
