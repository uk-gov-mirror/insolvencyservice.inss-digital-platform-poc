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
            Path = new Navigation { Controller = "Form", Id = FormConstants.FormId },
            PathName = "application",
            Sections = [
                new SectionModel
                {
                    Id = FormConstants.YourDetailsSectionId,
                    Name = "Your Details", 
                    PathName = "your-details",
                    Pages = [
                        new AddressModel
                        {
                            Id = FormConstants.YourDetailsPageId,
                            Path = new Navigation { Controller = "Address", Id = FormConstants.YourDetailsPageId },
                            PathName = "address"
                        },
                        new BankAccountModel
                        {
                            Id = FormConstants.PersonalAssetsPageId,
                            Path = new Navigation { Controller = "BankAccount", Id = FormConstants.PersonalAssetsPageId },
                            PathName = "bank-account"
                        }]
                }, 
                new SectionModel
                {
                    Id = FormConstants.AssetsSectionId,
                    Name = "Assets",
                    PathName = "assets",
                    Pages = [
                        new BankAccountModel
                        {
                            Id = FormConstants.AssetsPageId,
                            Path = new Navigation { Controller = "BankAccount",  Id = FormConstants.AssetsPageId },
                            PathName = "bank-account"
                        }]
                },
            ]
        });
    }
}