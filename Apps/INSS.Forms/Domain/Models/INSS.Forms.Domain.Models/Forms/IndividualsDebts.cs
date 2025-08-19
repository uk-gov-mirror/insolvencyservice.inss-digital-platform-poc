using INSS.Forms.Domain.Models.Composite;

namespace INSS.Forms.Domain.Models.Forms
{
    /// <summary>
    /// Represents a form model for an individual's debts.
    /// </summary>
    public class IndividualsDebts : Abstract.FormBase
    {
        /// <summary>
        /// Gets or sets the list of debt items associated with the individual.
        /// </summary>
        public IList<Debt> Debts { get; set; } = [];
    }
}
