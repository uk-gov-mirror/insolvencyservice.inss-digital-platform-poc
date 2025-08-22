using INSS.Forms.Domain.Models.Abstract;

namespace INSS.Forms.Application.Services.Data
{
    /// <summary>
    /// Defines methods for saving and retrieving DRO and DCRS form sets.
    /// </summary>
    public interface IFormRepository
    {
        /// <summary>
        /// Retrieves a set of forms by its form set instance ID asynchronously.
        /// </summary>
        /// <param name="formSetInstanceId">The unique identifier of the form set instance.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of <see cref="FormBase"/> instances if found; otherwise, <c>null</c>.
        /// </returns>
        Task<IEnumerable<FormBase>> GetFormSetAsync(Guid formSetInstanceId);

        /// <summary>
        /// Saves a form asynchronously.
        /// </summary>
        /// <param name="form">The form to save, derived from <see cref="FormBase"/>.</param>
        /// <returns>
        /// A task representing the asynchronous save operation.
        /// </returns>
        Task SaveFormAsync(FormBase form);
    }
}
