using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INSS.Web.Components.Services;

public interface IModelService<T>
{
    Task<T> LoadAsync();
    Task ValidateAsync(ModelStateDictionary modelState, T model);
    Task SaveAsync(T model);
}