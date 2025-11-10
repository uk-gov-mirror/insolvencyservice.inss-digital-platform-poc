using INSS.Web.Components.Models;

namespace INSS.Web.Components.Factories;

public interface IFormModelFactory
{
    Task<FormModel> CreateAsync();
}