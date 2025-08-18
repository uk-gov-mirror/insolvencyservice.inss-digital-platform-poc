using System.ComponentModel;

namespace INSS.Forms.Domain.Models.Enums
{
    /// <summary>
    /// Specifies the frequency types for payments.
    /// </summary>
    public enum PaymentFrequencyType
    {
        /// <summary>
        /// Payment is made every week.
        /// </summary>
        [Description("Weekly")]
        Weekly,

        /// <summary>
        /// Payment is made every month.
        /// </summary>
        [Description("Monthly")]
        Monthly,

        /// <summary>
        /// Payment is made on an ad-hoc basis.
        /// </summary>
        [Description("Ad-Hoc")]
        AdHoc
    }

    /// <summary>
    /// Specifies the types of companies.
    /// </summary>
    public enum CompanyType
    {
        /// <summary>
        /// A company that is registered as a limited company.
        /// </summary>
        [Description("Limited Company")]
        LimitedCompany,

        /// <summary>
        /// A company that is registered as a charity.
        /// </summary>
        [Description("Registered Charity")]
        RegisteredCharity,

        /// <summary>
        /// Any other type of company not listed.
        /// </summary>
        [Description("Other")]
        Other
    }

    /// <summary>
    /// Specifies a Yes or No value.
    /// </summary>
    public enum YesNoType
    {
        /// <summary>
        /// Represents a 'Yes' response.
        /// </summary>
        [Description("Yes")]
        Yes,

        /// <summary>
        /// Represents a 'No' response.
        /// </summary>
        [Description("No")]
        No
    }
}
