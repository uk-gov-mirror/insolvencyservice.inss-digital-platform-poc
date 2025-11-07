using INSS.Web.Components.Models;
using INSS.Web.Components.Utils;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Web.Components.Extensions;

public static class UrlHelperExtensions
{
    // TODO: Do this another way and ensure the cookie expires etc
    
    public static string? FormCookieHelper(this IUrlHelper urlHelper, FormModel model)
    {
        var routeInfoList = new List<RouteInfo>();

        foreach (var section in model.Sections)
        {
            var routeInfo = new RouteInfo { Id = section.Id, Url = section.GetSectionUrl(model) };
            routeInfoList.Add(routeInfo);
            
            foreach (var page in section.Pages)
            {
                routeInfo = new RouteInfo { Id = page.Id, Url = section.GetPageUrl(model, page) };
                routeInfoList.Add(routeInfo);
            }
        }
        
        var response = urlHelper.ActionContext.HttpContext.Response;
        
        response.Cookies.Append(RouteInfo.CookieName, System.Text.Json.JsonSerializer.Serialize(routeInfoList));

        return null;
    }
}