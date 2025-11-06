using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public interface IModelService<T>
{
    Task<T> LoadAsync(string? id);
    Task ValidateAsync(ModelStateDictionary modelState, T model);
    Task<Navigation> SaveAsync(T model);
}