using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents the association between a <see cref="DigitalService"/> and a <see cref="Section"/>, 
    /// including the sort order of the form section within the digital service.
    /// </summary>
    public class DigitalServiceSection
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="DigitalService"/>.
        /// </summary>
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(DigitalService))]
        public Guid DigitalServiceId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Section"/>.
        /// </summary>
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Section))]
        public Guid SectionId { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the form section within the digital service.
        /// </summary>
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the associated <see cref="DigitalService"/>.
        /// </summary>
        public DigitalService DigitalService { get; set; } = null!;

        /// <summary>
        /// Gets or sets the associated <see cref="Section"/>.
        /// </summary>
        public Section Section { get; set; } = null!;
    }
}
