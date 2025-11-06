using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public interface IModelService<TModel>
{
    Task<TModel> LoadAsync(string? id);
    Task ValidateAsync(ModelStateDictionary modelState, TModel model);
    Task<Navigation> SaveAsync(TModel model);
}