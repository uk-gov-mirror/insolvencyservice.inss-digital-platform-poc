using INSS.Forms.Application.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Application.Services.Api.Controllers
{
    /// <summary>
    /// API controller for configuration-related endpoints, including digital services, forms, and sections.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;

        private readonly IConfigurationRepository _configurationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="configurationRepository">The configuration repository.</param>
        public ConfigurationController(ILogger<ConfigurationController> logger, IConfigurationRepository configurationRepository)
        {
            _logger = logger;
            _configurationRepository = configurationRepository;
        }

        /// <summary>
        /// Retrieves a digital service by its name.
        /// </summary>
        /// <param name="serviceName">The name of the digital service.</param>
        /// <returns>The digital service if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("digital-service/{serviceName}")]
        public async Task<IActionResult> GetDigitalService(string serviceName)
        {
            var digitalService = await _configurationRepository.GetDigitalServiceByNameAsync(serviceName).ConfigureAwait(false);

            if (digitalService == null)
            {
                return NotFound();
            }

            return Ok(digitalService);
        }

        /// <summary>
        /// Retrieves the form identifiers associated with a digital service.
        /// </summary>
        /// <param name="digitalServiceId">The unique identifier of the digital service.</param>
        /// <returns>A list of form identifiers for the specified digital service.</returns>
        [HttpGet("digital-service/{digitalServiceId}/formids")]
        public async Task<IActionResult> GetDigitalServiceForms(Guid digitalServiceId)
        {
            var formIds = await _configurationRepository.GetFormIdentifiersForDigitalServiceAsync(digitalServiceId).ConfigureAwait(false);

            return Ok(formIds);
        }

        /// <summary>
        /// Retrieves all sections for a specified digital service.
        /// </summary>
        /// <param name="digitalServiceId">The unique identifier of the digital service.</param>
        /// <returns>A list of ordered sections for the specified digital service.</returns>
        [HttpGet("digital-service/{digitalServiceId}/sections")]
        public async Task<IActionResult> GetSections(Guid digitalServiceId)
        {
            var sections = await _configurationRepository.GetAllSectionsForDigitalServiceAsync(digitalServiceId).ConfigureAwait(false);

            return Ok(sections);
        }

        /// <summary>
        /// Retrieves all forms for a specified section.
        /// </summary>
        /// <param name="sectionId">The unique identifier of the section.</param>
        /// <returns>A list of ordered forms for the specified section.</returns>
        [HttpGet("section/{sectionId}/forms")]
        public async Task<IActionResult> GetSectionForms(Guid sectionId)
        {
            var forms = await _configurationRepository.GetAllFormsForSectionAsync(sectionId).ConfigureAwait(false);

            return Ok(forms);
        }
    }
}
