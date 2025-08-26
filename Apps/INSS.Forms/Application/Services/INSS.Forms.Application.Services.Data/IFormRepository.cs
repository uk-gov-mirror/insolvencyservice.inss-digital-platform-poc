using System.Net;

namespace INSS.Forms.Application.Services.Data
{
    /// <summary>
    /// Defines methods for saving and retrieving DRO and DCRS form sets.
    /// </summary>
    public interface IFormRepository
    {
        /// <summary>
        /// Retrieves the JSON representations of forms associated with the specified form set instance ID.
        /// </summary>
        /// <param name="formSetInstanceId">The unique identifier of the form set instance.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an enumerable collection of JSON strings for the forms.
        /// </returns>
        Task<IEnumerable<string>> GetJsonAsync(Guid formSetInstanceId);

        /// <summary>
        /// Saves a form asynchronously from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string representing the form data.</param>
        /// <returns>
        /// A task representing the asynchronous save operation. The task result contains the HTTP status code indicating the outcome of the save operation.
        /// </returns>
        Task<HttpStatusCode> SaveJsonAsync(string json);
    }
}
