using INSS.Web.Components.Services;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Index(string? id)
    {
        var model = await _modelService.LoadAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(T model)
    {
        await _modelService.ValidateAsync(ModelState, model);

        if (ModelState.IsValid)
        {
            var navigateTo = await _modelService.SaveAsync(model);
            return RedirectToAction(navigateTo.Action, navigateTo.Controller, new { Id = navigateTo.Id, prevId = Request.Path });
        }

        return View();
    }
}