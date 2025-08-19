using INSS.Forms.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models.Composite
{
    /// <summary>
    /// Represents a single debt, including details about the creditor, debt amounts, and payment information.
    /// </summary>
    public class Debt
    {
        /// <summary>
        /// Gets or sets the name of the creditor.
        /// </summary>
        public string CreditorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the initial amount of the debt.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? InitialDebt { get; set; }

        /// <summary>
        /// Gets or sets the date the debt was taken out.
        /// </summary>
        public DateTime? DateDebtTakenOut { get; set; }

        /// <summary>
        /// Gets or sets the current outstanding amount of the debt.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? OutstandingDebt { get; set; }

        /// <summary>
        /// Gets or sets the regular payment amount for the debt.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? PaymentAmount { get; set; }

        /// <summary>
        /// Gets or sets the frequency of payments for the debt.
        /// </summary>
        public PaymentFrequencyType? PaymentFrequency { get; set; }
    }
}
