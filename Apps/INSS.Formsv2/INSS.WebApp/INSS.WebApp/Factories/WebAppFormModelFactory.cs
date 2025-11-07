using INSS.Web.Components.Factories;
using INSS.Web.Components.Models;
using INSS.WebApp.Models;

namespace INSS.WebApp.Factories;

public sealed class WebAppFormModelFactory : IFormModelFactory
{
    public Task<FormModel> CreateAsync()
    {
        var form = new FormModel();
        PrefabModelSections.AddYourDetails(form);

        var section = new SectionModel { Name = "Assets", PathName = "assets" };
        form.AddSection(section);
        section.AddPage(new BankAccountModel());
        section.AddPage(new HomeValueModel());

        form.Initialize();

        /*
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        var json = JsonSerializer.Serialize(form, options);
        
        var form2 = JsonSerializer.Deserialize<FormModel>(json, options);
        */
        
        return Task.FromResult(form);
    }
}