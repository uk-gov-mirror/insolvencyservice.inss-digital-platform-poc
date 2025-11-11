using GovUk.Frontend.AspNetCore;
using INSS.Web.Components.Controllers;
using INSS.Web.Components.Models;
using INSS.Web.Components.Resolvers;
using INSS.Web.Components.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace INSS.Web.Components.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddComponents(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllersWithViews()
            .AddApplicationPart(typeof(BaseController<>).Assembly)
            .AddRazorRuntimeCompilation();
        builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options => 
        {
            options.FileProviders.Add(new EmbeddedFileProvider(typeof(BaseController<>).Assembly));
        });
        
        builder.Services.AddHttpClient();
        builder.Services.AddGovUkFrontend(options => options.Rebrand = true);
        builder.Services.AddSingleton<IJourneyService, JourneyService>();
        builder.Services.AddTransient<IModelService<BankAccountModel>, BankAccountService>();
        builder.Services.AddTransient<IModelService<AddressModel>, AddressService>();
        builder.Services.AddTransient<IModelService<FullNameModel>, FullNameService>();
        builder.Services.AddTransient<IModelService<SectionModel>, SectionService>();
        builder.Services.AddTransient<IModelService<FormModel>, FormService>();
        builder.Services.AddSingleton<IFormStateService, TestFormStateService>();
        builder.Services.AddSingleton<IUserSessionResolver, TestUserSessionResolver>();
        return builder;
    }
}