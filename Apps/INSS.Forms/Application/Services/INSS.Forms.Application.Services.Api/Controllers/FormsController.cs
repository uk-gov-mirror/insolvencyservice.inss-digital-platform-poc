using INSS.Forms.Application.Services.Api.Converters;
using INSS.Forms.Application.Services.Data;
using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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
        private static readonly Lazy<List<JsonDerivedType>> DerivedTypesLazy = new(GetFormBaseDerivedTypes, true);

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
        /// Retrieves a set of forms for the specified form set instance ID.
        /// </summary>
        /// <param name="formSetInstanceId">The unique identifier of the form set instance.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the serialized JSON of the form set.
        /// Returns HTTP 200 with the JSON content if successful.
        /// </returns>
        [HttpGet("{formSetInstanceId}")]
        public async Task<IActionResult> GetFormSet(Guid formSetInstanceId)
        {
            // Get the matching forms as json.
            IEnumerable<string> formSet = await _formRepository.GetJsonAsync(formSetInstanceId).ConfigureAwait(false);

            // Deserialization options for converting JSON strings to their derived types of <see cref="FormBase"/> objects.
            var deSerializerOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(), new BasePayloadConverter() },
                PropertyNameCaseInsensitive = true
            };

            // Converts a collection of JSON strings representing forms into a strongly-typed list of <see cref="FormBase"/> instances.
            // Each JSON string in <paramref name="formSet"/> is deserialized using the specified options.
            // Only successfully deserialized objects are added to the result list.
            var typedObjects = new List<FormBase>();
            foreach (var json in formSet)
            {
                var obj = JsonSerializer.Deserialize<FormBase>(json, deSerializerOptions);
                if (obj != null)
                    typedObjects.Add(obj);
            }

            // Using reflection get a cached list of types that are derived from the form base model class: FormBase
            List<JsonDerivedType> derivedTypes = DerivedTypesLazy.Value;

            // Serialization options for converting models that are derived from <see cref="FormBase"/> objects to JSON.
            // This is required so that the method returns a list of derived classes rather than the System.Json.Text default which is to return just the base class.
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new BasePayloadConverter() },
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                    {
                        typeInfo =>
                        {
                            if (typeInfo.Type == typeof(FormBase) && typeInfo.Kind == JsonTypeInfoKind.Object)
                            {
                                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                                {
                                    DerivedTypes = { }
                                };
                                foreach (var derivedType in derivedTypes)
                                {
                                    typeInfo.PolymorphismOptions.DerivedTypes.Add(derivedType);
                                }
                            }
                        }
                    }
                }
            };

            // Serializes the list of strongly-typed <see cref="FormBase"/> objects to JSON using the specified deserialization options.
            var jsonResult = JsonSerializer.Serialize(typedObjects, deSerializerOptions);

            /// Returns the serialized JSON result as an HTTP response with content type "application/json".
            return Content(jsonResult, "application/json");
        }

        /// <summary>
        /// Handles HTTP POST requests to save form data as JSON.
        /// Reads the request body, validates it, and saves the JSON to the repository.
        /// Returns appropriate HTTP status codes based on the result of the save operation.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save operation:
        /// <list type="bullet">
        /// <item><description>HTTP 200 OK if the data is saved successfully.</description></item>
        /// <item><description>HTTP 400 Bad Request if the request body is empty.</description></item>
        /// <item><description>HTTP 500 Internal Server Error or other status code if saving fails.</description></item>
        /// </list>
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostJson()
        {
            // Reads the request body as a JSON string and validates that it is not empty.
            using var reader = new StreamReader(Request.Body);
            var json = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                _logger.LogWarning("Request body is empty.");
                return BadRequest("Request body cannot be empty.");
            }

            // Saves the json to the repository
            var result = await _formRepository.SaveJsonAsync(json);

            if (result != System.Net.HttpStatusCode.OK && result != System.Net.HttpStatusCode.Created)
            {
                _logger.LogError("Failed to save form data. Status code: {StatusCode}", result);
                return StatusCode((int)result, "Failed to save form data.");
            }

            return Ok(new { Message = "Data saved successfully" });
        }

        /// <summary>
        /// Discovers all types derived from <see cref="FormBase"/> in the current application domain.
        /// Used for configuring polymorphic serialization and deserialization of form models.
        /// </summary>
        /// <returns>
        /// A list of <see cref="JsonDerivedType"/> representing all discovered non-abstract types derived from <see cref="FormBase"/>.
        /// e.g. AboutYou, CompanyDetails etc
        /// </returns>
        private static List<JsonDerivedType> GetFormBaseDerivedTypes()
        {
            var types = new List<JsonDerivedType>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] typeArr;
                try
                {
                    typeArr = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    typeArr = ex.Types.Where(t => t != null).Cast<Type>().ToArray();
                }
                foreach (var t in typeArr)
                {
                    if (t != null && typeof(FormBase).IsAssignableFrom(t) && !t.IsAbstract)
                    {
                        types.Add(new JsonDerivedType(t, t.Name));
                    }
                }
            }

            return types;
        }
    }
}
