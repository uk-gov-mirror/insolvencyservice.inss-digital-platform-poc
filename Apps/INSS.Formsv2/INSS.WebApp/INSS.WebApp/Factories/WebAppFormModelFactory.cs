using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;

namespace INSS.WebApp.Factories;

public sealed class WebAppFormModelFactory : IFormModelFactory
{
    public Task<FormModel> CreateAsync()
    {
        return Task.FromResult(new FormModel
        {
            Sections = [
                new SectionModel { Name = "Your Details" }, 
                new SectionModel { Name = "Assets" },
                new SectionModel { Name = "Creditors" }
            ]
        });
    }
}