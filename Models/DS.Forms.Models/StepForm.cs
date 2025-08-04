using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DS.Forms.Models
{
    /// <summary>
    /// Represents the association between a <see cref="Step"/> and a <see cref="Form"/>.
    /// </summary>
    public class StepForm
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Step"/>.
        /// </summary>
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Step))]
        public Guid StepId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Form"/>.
        /// </summary>
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Form))]
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the form within the step.
        /// </summary>
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the associated <see cref="Step"/>.
        /// </summary>
        public Step Step { get; set; } = null!;

        /// <summary>
        /// Gets or sets the associated <see cref="Form"/>.
        /// </summary>
        public Form Form { get; set; } = null!;
    }
}
