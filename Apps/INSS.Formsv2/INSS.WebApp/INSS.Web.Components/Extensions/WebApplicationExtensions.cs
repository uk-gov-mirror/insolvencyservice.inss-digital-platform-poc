using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Builder;

namespace INSS.Web.Components.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseComponents(this WebApplication app)
    {
        app.UseGovUkFrontend();
        return app;
    }
}