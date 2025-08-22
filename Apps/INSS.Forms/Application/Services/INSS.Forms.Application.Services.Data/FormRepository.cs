using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Forms;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public async Task<IEnumerable<FormBase>> GetFormSetAsync(Guid formSetInstanceId)
        {
            var query = _container.GetItemQueryIterator<dynamic>($"SELECT * FROM c WHERE c.formMetadata.formSetInstanceId = \"{formSetInstanceId}\"");

            var results = new List<FormBase>();
            while (query.HasMoreResults)
            {
                foreach (var item in await query.ReadNextAsync())
                {
                    string json = JsonConvert.SerializeObject(item);
                    var type = JObject.Parse(json)["formType"]?.ToString();

                    FormBase? formBase = type switch
                    {
                        "INSS.Forms.Domain.Models.Forms.AboutYou, INSS.Forms.Domain.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" => JsonConvert.DeserializeObject<AboutYou>(json),
                        "INSS.Forms.Domain.Models.Forms.CompanyDetails, INSS.Forms.Domain.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" => JsonConvert.DeserializeObject<CompanyDetails>(json),
                        "INSS.Forms.Domain.Models.Forms.IndividualsDebts, INSS.Forms.Domain.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" => JsonConvert.DeserializeObject<IndividualsDebts>(json),
                        "INSS.Forms.Domain.Models.Forms.IndividualsIncome, INSS.Forms.Domain.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" => JsonConvert.DeserializeObject<IndividualsIncome>(json),
                        _ => JsonConvert.DeserializeObject<FormBase>(json)
                    };

                    if (formBase != null)
                    {
                        results.Add(formBase);
                    }
                }
            }

            return results;
        }

        /// <inheritdoc />
        public async Task SaveFormAsync(FormBase form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            try
            {
                await _container.UpsertItemAsync(form, new PartitionKey(form.FormMetadata.FormSetInstanceId.ToString()));
            }
            catch (CosmosException cex)
            {
                _logger.LogError(cex, "Cosmos DB error while creating form: {Message}", cex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating form: {Message}", ex.Message);
                throw;
            }
        }

    }
}
