namespace INSS.Forms.Analytics.Models
{
    /// <summary>
    /// Represents the view model for error pages, providing request tracking information.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request that resulted in an error.
        /// </summary>
        /// <value>
        /// The request identifier, or <c>null</c> if no request ID is available.
        /// </value>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request ID should be displayed to the user.
        /// </summary>
        /// <value>
        /// <c>true</c> if the request ID is not null or empty; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
