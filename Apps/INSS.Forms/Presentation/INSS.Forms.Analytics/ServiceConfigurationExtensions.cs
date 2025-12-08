using INSS.Forms.Analytics.Options;
using INSS.Forms.Analytics.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace INSS.Forms.Analytics;

/// <summary>
/// Provides extension methods for configuring services in the dependency injection container.
/// </summary>
public static class ServiceConfigurationExtensions
{
    /// <summary>
    /// Configures authentication services with cookie-based authentication and JWT authentication service.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration containing authentication settings.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <remarks>
    /// This method sets up cookie authentication with secure defaults and registers the JWT authentication service.
    /// The authentication options are bound from the "Authentication" configuration section.
    /// </remarks>
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "UserManagement.Auth";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

        services.AddOptions<AuthOptions>()
            .Bind(configuration.GetSection("Authentication"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
        return services;
    }

    /// <summary>
    /// Configures analytics services including event tracking capabilities.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration containing analytics settings.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <remarks>
    /// This method registers the analytics options from the "Analytics" configuration section
    /// and sets up the Rybbit event tracker service for analytics functionality.
    /// </remarks>
    public static IServiceCollection AddAnalyticsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AnalyticsOptions>()
            .Bind(configuration.GetSection("Analytics"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IEventTracker, RybbitEventTracker>();

        return services;
    }
}
