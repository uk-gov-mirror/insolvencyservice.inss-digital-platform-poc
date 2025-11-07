using GovUk.Frontend.AspNetCore;
using INSS.Web.Components.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace INSS.Web.Components.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseComponents(this WebApplication app)
    {
        app.UseGovUkFrontend();
        
        var modelDataFactory = app.Services.GetRequiredService<IFormModelFactory>();

        var form = modelDataFactory.CreateAsync().Result;

        foreach (var section in form.Sections)
        {
            app.MapControllerRoute(name: form.PathName,
                pattern: $"{form.PathName}",
                defaults: new { controller = form.Path.Controller, action = form.Path.Action });
            
            foreach (var page in section.Pages)
            {
                app.MapControllerRoute(name: $"{section.PathName}-{page.PathName}",
                    pattern: $"{section.PathName}/{page.PathName}",
                    defaults: new { controller = page.Path.Controller, action = page.Path.Action });
            }
        }
        
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        
        return app;
    }
}