using INSS.Web.Components.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Web.Components.Extensions;

public static class UrlHelperExtensions
{
    public static string? OurUrl(this IUrlHelper urlHelper, FormModel model)
    {
        var routeInfoList = new List<RouteInfo>();

        foreach (var section in model.Sections)
        {
            foreach (var page in section.Pages)
            {
                var routeInfo = new RouteInfo
                {
                    Id = page.Id,
                    Url = $"/{section.PathName}/{page.PathName}"
                };
                routeInfoList.Add(routeInfo);
            }
        }
        
        //var url = $"/{model.PathName}/{model.PathName}/{model.Pages.First().PathName}";
        var response = urlHelper.ActionContext.HttpContext.Response;
        
        response.Cookies.Append("RouteId", System.Text.Json.JsonSerializer.Serialize(routeInfoList));

        return null;
    }
}

public sealed class RouteInfo
{
    public string Url { get; init; }
    public string Id { get; init; }
}