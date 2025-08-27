using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Configuration;

namespace INSS.Forms.Application.Services.Data
{
    /// <summary>
    /// Provides methods for accessing configuration data related to digital services, sections, and forms.
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Retrieves a <see cref="DigitalService"/> by its name.
        /// </summary>
        /// <param name="name">The name of the digital service.</param>
        /// <returns>
        /// A <see cref="DigitalService"/> instance if found; otherwise, <c>null</c>.
        /// </returns>
        Task<DigitalService?> GetDigitalServiceByNameAsync(string name);

        /// <summary>
        /// Retrieves the identifiers of forms associated with a specified digital service.
        /// </summary>
        /// <param name="digitalServiceId">The unique identifier of the digital service.</param>
        /// <returns>
        /// An enumerable collection of form <see cref="Guid"/>s.
        /// </returns>
        Task<IEnumerable<Guid>> GetFormIdentifiersForDigitalServiceAsync(Guid digitalServiceId);

        /// <summary>
        /// Retrieves all sections for a specified digital service, ordered by their sort order.
        /// </summary>
        /// <param name="digitalServiceId">The unique identifier of the digital service.</param>
        /// <returns>
        /// An enumerable collection of <see cref="OrderedSection"/> objects.
        /// </returns>
        Task<IEnumerable<OrderedSection>> GetAllSectionsForDigitalServiceAsync(Guid digitalServiceId);

        /// <summary>
        /// Retrieves all forms for a specified section, ordered by their sort order.
        /// </summary>
        /// <param name="sectionId">The unique identifier of the section.</param>
        /// <returns>
        /// An enumerable collection of <see cref="OrderedForm"/> objects.
        /// </returns>
        Task<IEnumerable<OrderedForm>> GetAllFormsForSectionAsync(Guid sectionId);
    }
}
