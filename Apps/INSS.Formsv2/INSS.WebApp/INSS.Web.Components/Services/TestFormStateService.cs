using INSS.Web.Components.Models;
using Microsoft.Extensions.Caching.Memory;

namespace INSS.Web.Components.Services;

public sealed class TestFormStateService : IFormStateService
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions());
    
    public Task<FormModel> GetAsync(string sessionId)
    {
        return Task.FromResult(_cache.Get<FormModel>(sessionId)!);
    }

    public Task SaveAsync(string sessionId, FormModel model)
    {
        _cache.Set(sessionId, model);
        return Task.CompletedTask;
    }
}