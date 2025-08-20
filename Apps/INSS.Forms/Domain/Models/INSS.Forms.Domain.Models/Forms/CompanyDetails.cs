using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Enums;

namespace INSS.Forms.Domain.Models.Forms
{
    /// <summary>
    /// Represents information about a person completing the form.
    /// </summary>
    public class CompanyDetails : FormBase
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the company (e.g., Limited, Partnership).
        /// </summary>
        public CompanyType? CompanyType { get; set; }

        /// <summary>
        /// Gets or sets the registration number of the company if applicable.
        /// </summary>
        public string CompaniesHouseReferenceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the charity reference number if applicable.
        /// </summary>
        public string CharityReferenceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        public Address CompanyAddress { get; set; } = new Address();

        /// <summary>
        /// Gets or sets the telephone number of the person.
        /// </summary>
        public string CompanyTelephone { get; set; } = string.Empty;
    }
}
