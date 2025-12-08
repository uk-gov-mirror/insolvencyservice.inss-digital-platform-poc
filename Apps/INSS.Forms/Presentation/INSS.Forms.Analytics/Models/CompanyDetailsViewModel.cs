using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Analytics.Models
{
    /// <summary>
    /// Represents the view model for company details form data.
    /// </summary>
    public class CompanyDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the company type indicator (charity or limited company).
        /// </summary>
        public string? IsCharityOrLtd { get; set; }

        /// <summary>
        /// Gets or sets the charity registration number.
        /// </summary>
        [Required()]
        public string? CharityNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the limited company registration number.
        /// </summary>
        public string? LimitedNumber { get; set; }
    }
}
