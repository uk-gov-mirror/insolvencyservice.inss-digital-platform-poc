using INSS.Forms.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models.Composite
{
    /// <summary>
    /// Represents an income entry with type, amount, frequency, and provider information.
    /// </summary>
    public class Income
    {
        /// <summary>
        /// Gets or sets the type of income.
        /// </summary>
        public IncomeType? IncomeType { get; set; }

        /// <summary>
        /// Gets or sets the amount of income.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the income.
        /// </summary>
        public IncomeFrequencyType? IncomeFrequency { get; set; }

        /// <summary>
        /// Gets or sets the provider of the income.
        /// </summary>
        public string Provider { get; set; } = string.Empty;
    }
}
