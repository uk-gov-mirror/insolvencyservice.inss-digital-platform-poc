using INSS.Web.Components.Extensions;
using INSS.Web.Components.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

// ReSharper disable Mvc.ViewNotResolved

namespace INSS.Web.Components.Controllers;

public class BaseController<T> : Controller
{
    private readonly IModelService<T> _modelService;

    protected BaseController(IModelService<T> modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var routeId = GetRouteId(Request);
        var model = await _modelService.LoadAsync(routeId);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(T model)
    {
        await _modelService.ValidateAsync(ModelState, model);

        if (ModelState.IsValid)
        {
            var navigateTo = await _modelService.SaveAsync(model);
            return Redirect(navigateTo);
            //return RedirectToAction(navigateTo.Action, navigateTo.Controller);//, new { Id = navigateTo.Id });
        }

        return View(model);
    }

    private static string? GetRouteId(HttpRequest request)
    {
        string? routeId = null;
        var requestUrl = request.Path.Value;
        
        if (requestUrl is not null && request.Cookies.TryGetValue("RouteId", out var cookieValue))
        {
            var routeInfoList = System.Text.Json.JsonSerializer.Deserialize<List<RouteInfo>>(cookieValue);

            var routeInfo = routeInfoList?.FirstOrDefault(ri => requestUrl.EndsWith(ri.Url));
            
            routeId = routeInfo?.Id;
        }
        
        return routeId;
    }
}