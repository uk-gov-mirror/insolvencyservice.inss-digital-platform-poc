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
            Sections = [
                new SectionModel
                {
                    Id = FormConstants.YourDetailsSectionId,
                    Name = "Your Details", 
                    Pages = [
                        new AddressModel
                        {
                            Id = FormConstants.YourDetailsPageId,
                            Path = new Navigation { Controller = "Address", Id = FormConstants.YourDetailsPageId }
                        },
                        new BankAccountModel
                        {
                            Id = FormConstants.PersonalAssetsPageId,
                            Path = new Navigation { Controller = "BankAccount", Id = FormConstants.PersonalAssetsPageId }
                        }]
                }, 
                new SectionModel
                {
                    Id = FormConstants.AssetsSectionId,
                    Name = "Assets", 
                    Pages = [
                        new BankAccountModel
                        {
                            Id = FormConstants.AssetsPageId,
                            Path = new Navigation { Controller = "BankAccount",  Id = FormConstants.AssetsPageId }
                        }]
                },
            ]
        });
    }
}