using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;

namespace INSS.WebApp.Factories;

public sealed class WebAppFormModelFactory : IFormModelFactory
{
    private readonly IJourneyService _journeyService = new JourneyService();
    
    public Task<FormModel> CreateAsync()
    {
        return Task.FromResult(new FormModel
        {
            Id = FormConstants.FormId,
            Sections = [
                new SectionModel
                {
                    Id = FormConstants.YourDetailsSectionId,
                    Name = "Your Details", 
                    Url = _journeyService.GetActionRedirect(null),
                    Pages = [
                        new PageModel
                        {
                            Id = FormConstants.YourDetailsPageId,
                            Questions = [
                                new AddressModel
                                {
                                    Id = FormConstants.YourDetailsAddressId,
                                    Url = new Navigation { Controller = "Form" }
                                }
                            ]
                        }]
                }, 
                new SectionModel
                {
                    Id = FormConstants.AssetsSectionId,
                    Name = "Assets", 
                    Url = _journeyService.GetActionRedirect(null),
                    Pages = [
                        new PageModel
                        {
                            Id = FormConstants.AssetsPageId,
                            Questions = [new BankAccountModel
                            {
                                Id = FormConstants.AssetsBankAccountId,
                                Url = new Navigation { Controller = "Form" }
                            }]
                        }]
                },
            ]
        });
    }
}

public interface IJourneyService
{
    Navigation GetActionRedirect(BaseModel model);
}

public sealed class JourneyService : IJourneyService
{
    public Navigation GetActionRedirect(BaseModel model)
    {
        return new Navigation { Controller = "Address" };
    }
}