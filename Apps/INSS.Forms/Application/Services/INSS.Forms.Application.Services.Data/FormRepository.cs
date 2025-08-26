using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Forms;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;

namespace INSS.Forms.Application.Services.Data
{
    /// <inheritdoc />
    public class FormRepository : IFormRepository
    {
        private readonly ILogger<FormRepository> _logger;
        private readonly Container _container;

        public FormRepository(ILogger<FormRepository> logger, CosmosClient cosmosClient)
        {
            _logger = logger;

            Database database = cosmosClient.GetDatabase("Forms");
            _container = database.GetContainer("FormInstance");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetJsonAsync(Guid formSetInstanceId)
        {
            var query = _container.GetItemQueryIterator<dynamic>($"SELECT * FROM c WHERE c.formMetadata.formSetInstanceId = \"{formSetInstanceId}\"");

            var results = new List<string>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                foreach (var item in response)
                {
                    string json = JsonConvert.SerializeObject(item);
                    results.Add(json);
                }
            }

            return results;
        }

        /// <inheritdoc />
        public async Task<HttpStatusCode> SaveJsonAsync(string json)
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var partitionKey = root.GetProperty("formMetadata").GetProperty("formSetInstanceId").GetString();

            _logger.LogTrace($"SaveFormAsync(string json) - json:{Environment.NewLine}{json}");

            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentException("Invalid JSON: Missing formSetInstanceId in formMetadata.");
            }

            try
            {
                var item = JsonConvert.DeserializeObject<object>(json);
                var response = await _container.UpsertItemAsync(item, new PartitionKey(partitionKey)).ConfigureAwait(false);
                return response.StatusCode;
            }
            catch (CosmosException cex)
            {
                _logger.LogError(cex, "Cosmos DB error while creating form from JSON: {Message}", cex.Message);
                return HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating form from JSON: {Message}", ex.Message);
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
