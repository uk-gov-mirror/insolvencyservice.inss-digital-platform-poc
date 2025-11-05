using INSS.Web.Components.Models;
using INSS.Web.Components.Services;

namespace INSS.Web.Components.Controllers;

public class AddressController : BaseController<AddressModel>
{
    public AddressController(IModelService<AddressModel> addressService) : base(addressService)
    {
    }
}