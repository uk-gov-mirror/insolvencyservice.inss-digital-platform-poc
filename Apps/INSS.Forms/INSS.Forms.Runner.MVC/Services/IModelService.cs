using INSS.Forms.Runner.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace INSS.Forms.Runner.MVC.Services
{
    public interface IModelService<T>
    {
        Task<T> LoadAsync();
        Task ValidateAsync(ModelStateDictionary modelState, T model);
        Task SaveAsync(T model);
    }
}