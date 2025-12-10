using INSS.Forms.Analytics.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace INSS.Forms.Analytics.Services
{
    public class RybbitEventTracker(ILogger<RybbitEventTracker> logger, HttpClient httpClient, IOptions<AnalyticsOptions> options) : IEventTracker
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<RybbitEventTracker> _logger = logger;
        private readonly AnalyticsOptions _options = options.Value;

        /// <inheritdoc/>
        public async Task TrackEventAsync(string eventType, string eventName, Dictionary<string, object> properties)
        {
            if(_options.RybbitEnabled is false)
            {
                return;
            }

            Dictionary<string, object> eventData = new()
            {
                ["site_id"] = _options.RybbitSiteId ?? string.Empty,
                ["type"] = eventType,
                ["event_name"] = eventName,
                ["properties"] = JsonSerializer.Serialize(properties)
            };

            using StringContent content = new(JsonSerializer.Serialize(eventData), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _httpClient.PostAsync($"{_options.RybbitBaseUrl}/api/track", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to track event {EventName}. Status Code: {StatusCode}, Error: {Error}", eventName, response.StatusCode, error);
            }
        }
    }
}
