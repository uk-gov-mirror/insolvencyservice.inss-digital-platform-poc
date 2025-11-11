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

        var sectionAboutYou = new SectionModel { Name = "About You", PathName = "about-you" };
        form.AddSection(sectionAboutYou);
        sectionAboutYou.AddPage(new FullNameModel());

        form.Initialize();

        /*
        // Example serialize and deserialize
        var json = form.Serialize();
        
        var form2 = FormModel.Deserialize(json);
        */

        return Task.FromResult(form);
    }
}