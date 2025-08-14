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
        weekly,

        /// <summary>
        /// Payment is made every month.
        /// </summary>
        [Description("Monthly")]
        monthly,

        /// <summary>
        /// Payment is made on an ad-hoc basis.
        /// </summary>
        [Description("Ad-Hoc")]
        adhoc
    }
}
