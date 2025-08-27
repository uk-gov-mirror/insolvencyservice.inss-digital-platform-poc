using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace INSS.Forms.Application.Services.Data
{
    /// <inheritdoc />
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ILogger<ConfigurationRepository> _logger;
        private readonly ConfigurationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging errors and information.</param>
        /// <param name="dbContext">The configuration database context.</param>
        public ConfigurationRepository(ILogger<ConfigurationRepository> logger, ConfigurationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<DigitalService?> GetDigitalServiceByNameAsync(string name)
        {
            DigitalService? digitalService = null;

            try
            {
                digitalService = await _dbContext.DigitalServices
                    .FirstOrDefaultAsync(ds => ds.Name == name);

            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the DigitalService by name.");
            }

            return digitalService;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Guid>> GetFormIdentifiersForDigitalServiceAsync(Guid digitalServiceId)
        {
            IEnumerable<Guid> formIds = Enumerable.Empty<Guid>();

            try
            {
                var sectionIds = await _dbContext.DigitalServiceSections
                    .Where(ds => ds.DigitalServiceId == digitalServiceId)
                    .Select(dsSection => dsSection.SectionId)
                    .ToListAsync();

                formIds = await _dbContext.SectionForms
                    .Where(sf => sectionIds.Contains(sf.SectionId))
                    .Select(sf => sf.Form.Id)
                    .Distinct()
                    .ToListAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving form identifiers for the digital service.");
            }

            return formIds;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderedSection>> GetAllSectionsForDigitalServiceAsync(Guid digitalServiceId)
        {
            IEnumerable<OrderedSection> orderedSections = Enumerable.Empty<OrderedSection>();

            try
            {
                orderedSections = await _dbContext.DigitalServiceSections
                    .Where(ds => ds.DigitalServiceId == digitalServiceId)
                    .Include(ds => ds.Section)
                    .OrderBy(ds => ds.SortOrder)
                    .Select(ds => new OrderedSection
                    {
                        Id = ds.Section.Id,
                        Name = ds.Section.Name,
                        Description = ds.Section.Description,
                        SortOrder = ds.SortOrder
                    })
                    .ToListAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving sections for the digital service.");
            }

            return orderedSections;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderedForm>> GetAllFormsForSectionAsync(Guid sectionId)
        {
            IEnumerable<OrderedForm> orderedForms = Enumerable.Empty<OrderedForm>();

            try
            {
                orderedForms = await _dbContext.SectionForms
                    .Where(s => s.SectionId == sectionId)
                    .Include(s => s.Form)
                    .OrderBy(s => s.SortOrder)
                    .Select(s => new OrderedForm
                    {
                        Id = s.Form.Id,
                        Name = s.Form.Name,
                        UriPath = s.Form.UriPath,
                        SortOrder = s.SortOrder
                    })
                    .ToListAsync();

            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving forms for the section.");
            }

            return orderedForms;
        }
    }
}
