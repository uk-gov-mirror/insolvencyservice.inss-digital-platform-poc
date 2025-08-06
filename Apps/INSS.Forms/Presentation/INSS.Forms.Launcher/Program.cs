using INSS.Forms.Launcher.WebApp.Components;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Environment.IsDevelopment())
{
    // Register the self-signed certificate for development purposes.
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(6001, listenOptions =>
        {
            listenOptions.UseHttps(httpsOptions =>
            {
                httpsOptions.ServerCertificateSelector = (context, hostname) =>
                {
                    string selfSignedCertificatePath = Path.Combine(AppContext.BaseDirectory, "SelfSignedCerts");
                    string selfSignedCertificatePassword = builder.Configuration["Certificates:SelfSignedPassword"] ?? string.Empty;

                    return new X509Certificate2(Path.Combine(selfSignedCertificatePath, $"{hostname}.pfx"), selfSignedCertificatePassword);
                };
            });
        });
    });
}

// Register with the DI container the services required for the application.
builder.Services.AddHttpContextAccessor();

// The services are built, so we can now register the components.
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
    .AddInteractiveServerRenderMode();

app.Run();
