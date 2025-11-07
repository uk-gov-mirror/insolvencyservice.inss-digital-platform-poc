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
            Sections = [
                PrefabModelSections.YourDetails, 
                new SectionModel
                {
                    Name = "Assets",
                    PathName = "assets",
                    Pages = [new BankAccountModel(), new HomeValueModel()]
                }
            ]
        });
    }
}