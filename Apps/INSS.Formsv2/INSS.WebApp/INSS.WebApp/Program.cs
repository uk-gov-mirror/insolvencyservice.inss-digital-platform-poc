using INSS.Web.Components.Extensions;
using INSS.Web.Components.Factories;
using INSS.WebApp.Factories;

var builder = WebApplication.CreateBuilder(args);

builder.AddComponents();

builder.Services.AddTransient<IFormModelFactory, WebAppFormModelFactory>();

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