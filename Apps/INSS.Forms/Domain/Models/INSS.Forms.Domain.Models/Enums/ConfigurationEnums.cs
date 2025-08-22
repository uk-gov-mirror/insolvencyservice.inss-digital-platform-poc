using System.ComponentModel;

namespace INSS.Forms.Domain.Models.Enums
{
    /// <summary>
    /// Specifies the type of digital service provided.
    /// </summary>
    /// <remarks>This enumeration is used to identify different types of digital services, such as  Debt
    /// Relief Order (DRO) and Director Conduct Reporting Service (DCRS). Each value  corresponds to a specific service
    /// type.</remarks>
    public enum DigitalServiceType
    {
        /// <summary>
        /// Represents a Debt Relief Order (DRO) service.
        /// </summary>
        [Description("Debt Relief Order")]
        Dro,
        /// <summary>
        /// Represents a Director Conduct Reporting Service (DCRS).
        /// </summary>
        [Description("Director Conduct Reporting Service")]
        Dcrs
    }
}
