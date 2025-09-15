
using INSS.Forms.BCL.Services;
using INSS.Forms.Runner.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.MaxValue;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IFormMetadataService, FormMetadataService>();
builder.Services.AddScoped<IFormApiClient, FormApiClient>();


builder.Services.AddRazorComponents();

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForErrors: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseSession();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(typeof(INSS.Forms.BCL.RegisterSharedComponents).Assembly); //This is needed to register the shared components from the INSS.Forms.Components assembly, without this the components routes return a 404.

app.MapControllers();


app.Run();
