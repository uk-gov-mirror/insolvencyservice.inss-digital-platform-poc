namespace INSS.Forms.Analytics.Models
{
    /// <summary>
    /// Represents the view model for registering director information in a company.
    /// </summary>
    public class RegisterDirectorViewModel
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The company name, or <c>null</c> if not specified.
        /// </value>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the director.
        /// </summary>
        /// <value>
        /// The director's name, or <c>null</c> if not specified.
        /// </value>
        public string? DirectorName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the director is a majority shareholder.
        /// </summary>
        /// <value>
        /// <c>true</c> if the director is a majority shareholder; <c>false</c> if not; 
        /// <c>null</c> if the status is unknown or not specified.
        /// </value>
        public bool? IsMajorityShareholder { get; set; }

        /// <summary>
        /// Gets or sets the percentage of shares owned by the director.
        /// </summary>
        /// <value>
        /// The share percentage as a decimal value (e.g., 0.25 for 25%), 
        /// or <c>null</c> if not specified.
        /// </value>
        public decimal? SharePercentage { get; set; }
    }
}
