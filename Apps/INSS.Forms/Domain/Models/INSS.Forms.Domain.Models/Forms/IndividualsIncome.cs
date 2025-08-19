using INSS.Forms.Domain.Models.Composite;

namespace INSS.Forms.Domain.Models.Forms
{
    public class IndividualsIncome : Abstract.FormBase
    {
        /// <summary>
        /// Gets or sets the list of income associated with the individual.
        /// </summary>
        public IList<Income> Income { get; set; } = [];
    }
}
