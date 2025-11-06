using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;

namespace INSS.WebApp.Factories;

public sealed class WebAppFormModelFactory : IFormModelFactory
{
    public Task<FormModel> CreateAsync()
    {
        return Task.FromResult(new FormModel
        {
            Id = FormConstants.FormId,
            Path = new Navigation { Controller = "Form" },
            Sections = [
                new SectionModel
                {
                    Id = FormConstants.YourDetailsSectionId,
                    Name = "Your Details", 
                    Pages = [
                        new PageModel
                        {
                            Id = FormConstants.YourDetailsPageId,
                            Path = new Navigation { Controller = "Address" },
                            Question = new AddressModel
                            {
                                Id = FormConstants.YourDetailsAddressId
                            }
                        }]
                }, 
                new SectionModel
                {
                    Id = FormConstants.AssetsSectionId,
                    Name = "Assets", 
                    Pages = [
                        new PageModel
                        {
                            Id = FormConstants.AssetsPageId,
                            Path = new Navigation { Controller = "BankAccount" },
                            Question = new BankAccountModel
                            {
                                Id = FormConstants.AssetsBankAccountId
                            }
                        }]
                },
            ]
        });
    }
}