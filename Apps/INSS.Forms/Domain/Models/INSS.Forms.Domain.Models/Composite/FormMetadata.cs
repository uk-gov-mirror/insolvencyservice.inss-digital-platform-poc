namespace INSS.Forms.Domain.Models.Composite
{
    /// <summary>
    /// Represents metadata information for a form, including identifiers and user context.
    /// </summary>
    public class FormMetadata
    {
        /// <summary>
        /// Gets or sets the unique identifier for the form definition.
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the specific form instance.
        /// </summary>
        public Guid FormInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the username associated with the form.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the return URL to redirect after form completion.
        /// </summary>
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
