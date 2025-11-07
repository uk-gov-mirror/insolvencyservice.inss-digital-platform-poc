using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using INSS.WebApp.Models;

namespace INSS.WebApp.Factories;

public sealed class WebAppFormModelFactory : IFormModelFactory
{
    public Task<FormModel> CreateAsync()
    {
        return Task.FromResult(new FormModel
        {
            Path = new Navigation { Controller = "Form" },
            PathName = "tasks",
            Sections = [
                new SectionModel
                {
                    Name = "Your Details", 
                    PathName = "your-details",
                    Pages = [
                        new AddressModel { PathName = "address" },
                        new BankAccountModel { PathName = "bank-account" }
                    ]
                }, 
                new SectionModel
                {
                    Name = "Assets",
                    PathName = "assets",
                    Pages = [
                        new BankAccountModel { PathName = "bank-account" },
                        new HomeValueModel { PathName = "home-value" }
                    ]
                }
            ]
        });
    }
}