using INSS.Forms.Components.Abstract;
using INSS.Forms.Components.Services;
using INSS.Forms.Runner.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IFormPageNavigatorService, FormPageNavigatorService>();

builder.Services.AddScoped<IFormMetadataService, FormMetadataService>();

builder.Services.AddScoped<IFormApiClient, FormApiClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(typeof(INSS.Forms.Components.RegisterSharedComponents).Assembly) //This is needed to register the shared components from the INSS.Forms.Components assembly, without this the components routes return a 404.
    .AddInteractiveServerRenderMode();

app.Run();
