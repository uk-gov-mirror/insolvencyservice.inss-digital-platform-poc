using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DS.Forms.Models
{
    /// <summary>
    /// Represents the association between a <see cref="DigitalService"/> and a <see cref="Step"/>, 
    /// including the sort order of the step within the digital service.
    /// </summary>
    public class DigitalServiceStep
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="DigitalService"/>.
        /// </summary>
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(DigitalService))]
        public Guid DigitalServiceId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Step"/>.
        /// </summary>
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Step))]
        public Guid StepId { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the step within the digital service.
        /// </summary>
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the associated <see cref="DigitalService"/>.
        /// </summary>
        public DigitalService DigitalService { get; set; } = null!;

        /// <summary>
        /// Gets or sets the associated <see cref="Step"/>.
        /// </summary>
        public Step Step { get; set; } = null!;
    }
}
