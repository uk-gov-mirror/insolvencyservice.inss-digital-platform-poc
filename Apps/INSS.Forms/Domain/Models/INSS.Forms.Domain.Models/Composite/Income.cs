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
        [Required(ErrorMessage = "Income Type is required")]
        public IncomeType? IncomeType { get; set; }

        /// <summary>
        /// Gets or sets the amount of income.
        /// </summary>
        [Required(ErrorMessage = "Amount is required")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the income.
        /// </summary>
        [Required(ErrorMessage = "Income Frequency is required")]
        public IncomeFrequencyType? IncomeFrequency { get; set; }

        /// <summary>
        /// Gets or sets the provider of the income.
        /// </summary>
        [Required(ErrorMessage = "Provider is required")]
        public string Provider { get; set; } = string.Empty;
    }
}
