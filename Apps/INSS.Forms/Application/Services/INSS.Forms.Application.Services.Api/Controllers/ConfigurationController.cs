using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.Application.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;

        public ConfigurationController(ILogger<ConfigurationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dummy Method - Retrieves a collection of configuration strings.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{string}"/> containing five configuration strings.
        /// </returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new string(index.ToString())).ToArray();
        }
    }
}
