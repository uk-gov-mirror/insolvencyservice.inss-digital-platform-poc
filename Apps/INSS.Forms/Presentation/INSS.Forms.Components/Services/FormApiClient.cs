using INSS.Forms.Components.Abstract;
using INSS.Forms.Domain.Models.Abstract;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace INSS.Forms.Components.Services
{

    /// <inheritdoc />
    public class FormApiClient : IFormApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FormApiClient> _logger;

        private JsonSerializerOptions serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="FormApiClient"/> class with the specified HTTP client and
        /// logger.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> instance used to send HTTP requests to the API. This parameter cannot be <see
        /// langword="null"/>.</param>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance used for logging diagnostic messages. This parameter
        /// cannot be <see langword="null"/>.</param>
        public FormApiClient(HttpClient httpClient, ILogger<FormApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<bool> PostFormDataAsync(FormBase formData, string apiUrl)
        {
            if (formData == null || string.IsNullOrWhiteSpace(apiUrl))
                return false;

            string json = string.Empty;
            try
            {
                json = JsonSerializer.Serialize(formData, formData.GetType(), serializerOptions);

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed calling: {ApiUrl}", apiUrl);
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("Request timed out or was canceled calling: {ApiUrl}", apiUrl);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Deserialization failed for JSON: {json} while posting to {ApiUrl}", json, apiUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while posting form data to {ApiUrl}", apiUrl);
            }

            return false;
        }
    }
}
