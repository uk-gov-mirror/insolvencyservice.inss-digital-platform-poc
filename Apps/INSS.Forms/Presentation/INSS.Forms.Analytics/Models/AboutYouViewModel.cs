namespace INSS.Forms.Analytics.Models
{
    /// <summary>
    /// Represents the view model for collecting personal information about a user.
    /// This model is used to capture basic personal details in the About You form.
    /// </summary>
    public class AboutYouViewModel
    {
        /// <summary>
        /// Gets or sets the user's title (e.g., Mr, Mrs, Ms, Dr).
        /// </summary>
        /// <value>
        /// The title of the user, or null if not provided.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        /// <value>
        /// The first name of the user, or null if not provided.
        /// </value>
        public string? FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        /// <value>
        /// The last name of the user, or null if not provided.
        /// </value>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        /// <value>
        /// The phone number of the user, or null if not provided.
        /// </value>
        public string? PhoneNumber { get; set; }
    }
}
