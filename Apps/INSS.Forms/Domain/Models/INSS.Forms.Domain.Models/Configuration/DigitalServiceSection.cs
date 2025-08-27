using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents the association between a <see cref="DigitalService"/> and a <see cref="Section"/>, 
    /// including the sort order of the form section within the digital service.
    /// </summary>
    [Table("digital_service_section")]
    public class DigitalServiceSection
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="DigitalService"/>.
        /// </summary>
        [Key, Column("digital_service_id", Order = 0)]
        [ForeignKey(nameof(DigitalService))]
        public Guid DigitalServiceId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Section"/>.
        /// </summary>
        [Key, Column("section_id", Order = 1)]
        [ForeignKey(nameof(Section))]
        public Guid SectionId { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the form section within the digital service.
        /// </summary>
        [Key, Column("sort_order")]
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the associated <see cref="Section"/>.
        /// </summary>
        public Section Section { get; set; } = null!;
    }
}
