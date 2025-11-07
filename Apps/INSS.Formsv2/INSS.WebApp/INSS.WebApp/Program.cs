using INSS.Web.Components.Extensions;
using INSS.Web.Components.Factories;
using INSS.Web.Components.Services;
using INSS.WebApp.Factories;
using INSS.WebApp.Models;
using INSS.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddComponents();

builder.Services.AddTransient<IFormModelFactory, WebAppFormModelFactory>();

builder.Services.AddTransient<IModelService<HomeValueModel>, HomeValueService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseComponents();

app.Run();