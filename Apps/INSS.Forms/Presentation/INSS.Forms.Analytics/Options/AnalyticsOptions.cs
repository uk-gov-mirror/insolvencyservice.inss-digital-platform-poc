using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Analytics.Options;

/// <summary>
/// Represents the analytics configuration options for the application.
/// </summary>
public class AnalyticsOptions
{
    /// <summary>
    /// Gets or sets the base URL for the Rybbit analytics service.
    /// </summary>
    /// <value>
    /// The base URL used to connect to the Rybbit analytics platform.
    /// This value is required and must be a valid URL.
    /// </value>
    [Required]
    public string? RybbitBaseUrl { get; set; } 

    /// <summary>
    /// Gets or sets the site identifier for the Rybbit analytics service.
    /// </summary>
    /// <value>
    /// The unique site ID used to identify this application within the Rybbit analytics platform.
    /// This value is required and must be provided by the Rybbit service configuration.
    /// </value>
    [Required]
    public string? RybbitSiteId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Rybbit analytics service is enabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if Rybbit analytics tracking should be active; otherwise, <c>false</c>.
    /// This value is required and controls whether Rybbit analytics scripts are loaded and events are tracked.
    /// </value>
    public bool RybbitEnabled { get; set; }

    /// <summary>
    /// Gets or sets the base URL for the Plausible analytics service.
    /// </summary>
    /// <value>
    /// The base URL used to connect to the Plausible analytics platform.
    /// This value is required and must be a valid URL.
    /// </value>
    [Required]
    public string? PlausibleBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Plausible analytics service is enabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if Plausible analytics tracking should be active; otherwise, <c>false</c>.
    /// This value is required and controls whether Plausible analytics scripts are loaded and events are tracked.
    /// </value>
    public bool PlausibleEnabled { get; set; }

    /// <summary>
    /// Gets or sets the base URL for the Google Tag service.
    /// </summary>
    /// <value>
    /// The base URL used to connect to the Google Tag platform.
    /// </value>
    public string? GoogleTagBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the site identifier for Google Analytics 4 (GA4).
    /// </summary>
    /// <value>
    /// The unique site ID used to identify this application within the Google Analytics 4 platform.
    /// </value>
    public string? GA4SiteId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Google Analytics 4 (GA4) is enabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if Google Analytics 4 tracking should be active; otherwise, <c>false</c>.
    /// Controls whether GA4 analytics scripts are loaded and events are tracked.
    /// </value>
    public bool GA4Enabled { get; set; }

    /// <summary>
    /// Gets or sets the site identifier for Google Tag Manager (GTM).
    /// </summary>
    /// <value>
    /// The unique site ID used to identify this application within the Google Tag Manager platform.
    /// </value>
    public string? GTMSiteId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Google Tag Manager (GTM) is enabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if Google Tag Manager tracking should be active; otherwise, <c>false</c>.
    /// Controls whether GTM scripts are loaded and events are tracked.
    /// </value>
    public bool GTMEnabled { get; set; }
}
