using INSS.Forms.Application.Services.Data;
using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace INSS.Forms.Application.Services.Api.Controllers
{
    /// <summary>
    /// API controller for managing form sets and individual forms.
    /// Provides endpoints to retrieve and save forms.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly ILogger<FormsController> _logger;
        private readonly IFormRepository _formRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging controller actions.</param>
        /// <param name="formRepository">The form repository for data access.</param>
        public FormsController(ILogger<FormsController> logger, IFormRepository formRepository)
        {
            _logger = logger;
            _formRepository = formRepository;
        }

        /// <summary>
        /// Retrieves a set of forms by the specified form set instance ID.
        /// </summary>
        /// <param name="formSetInstanceId">The unique identifier of the form set instance.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the serialized form set if found; otherwise, a NotFound result.
        /// </returns>
        [HttpGet("{formSetInstanceId}")]
        public async Task<IActionResult> GetFormSet(Guid formSetInstanceId)
        {
            var formSet = await _formRepository.GetFormSetAsync(formSetInstanceId).ConfigureAwait(false);

            if (formSet == null)
            {
                _logger.LogWarning("Form set with ID {FormSetInstanceId} not found.", formSetInstanceId);
                return NotFound();
            }

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                        {
                            typeInfo =>
                            {
                                if (typeInfo.Type == typeof(FormBase))
                                {
                                    typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                                    {
                                        DerivedTypes =
                                        {
                                            // Add all derived form types here
                                            new JsonDerivedType(typeof(AboutYou), "AboutYou"),
                                            new JsonDerivedType(typeof(CompanyDetails), "CompanyDetails"),
                                            new JsonDerivedType(typeof(IndividualsDebts), "IndividualsDebts"),
                                            new JsonDerivedType(typeof(IndividualsIncome), "IndividualsIncome"),
                                        }
                                    };
                                }
                            }
                        }
                }
            };

            var json = JsonSerializer.Serialize(formSet, serializerOptions);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Saves a form sent in the request body.
        /// </summary>
        /// <param name="formElement">The JSON element representing the form data.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save operation.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostForm([FromBody] JsonElement formElement)
        {
            if (!formElement.TryGetProperty("formType", out JsonElement formTypeElement) ||
                formTypeElement.ValueKind != JsonValueKind.String)
            {
                _logger.LogWarning("FormType is missing or invalid in the request body.");
                return BadRequest("FormType is required.");
            }

            string formType = formTypeElement.GetString()!;
            Type? targetType = Type.GetType(formType, throwOnError: false, ignoreCase: true);

            if (targetType is null)
            {
                _logger.LogError("Unknown formType: {FormType}", formType);
                return BadRequest($"Unknown formType: {formType}");
            }

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var form = (FormBase?)formElement.Deserialize(targetType, serializerOptions);

            if (form is null)
            { 
                _logger.LogError("Failed to deserialize the form data of type {FormType} into type: FormBase", formType);
                return BadRequest($"Failed to deserialize the form data of type {formType} into type: FormBase");
            }

            await _formRepository.SaveFormAsync(form);

            return Ok(new { Message = "Data saved successfully" });
        }
    }
}
