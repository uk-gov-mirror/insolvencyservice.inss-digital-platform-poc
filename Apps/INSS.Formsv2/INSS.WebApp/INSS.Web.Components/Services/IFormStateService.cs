using INSS.Web.Components.Models;

namespace INSS.Web.Components.Services;

public interface IFormStateService
{
    Task<FormModel> GetAsync(string sessionId);
    
    Task SaveAsync(string sessionId, FormModel model);
}