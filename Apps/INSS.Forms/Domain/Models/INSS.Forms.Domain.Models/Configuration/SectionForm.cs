using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents the association between a <see cref="Configuration.Section"/> and a <see cref="Form"/>.
    /// </summary>
    [Table("section_form")]
    public class SectionForm
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Configuration.Section"/>.
        /// </summary>
        [Key, Column("section_id", Order = 0)]
        [ForeignKey(nameof(Configuration.Section))]
        public Guid SectionId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated <see cref="Form"/>.
        /// </summary>
        [Key, Column("form_id", Order = 1)]
        [ForeignKey(nameof(Form))]
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the form within the section.
        /// </summary>
        [Column("sort_order")]
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the associated <see cref="Configuration.Section"/>.
        /// </summary>
        public Section Section { get; set; } = null!;

        /// <summary>
        /// Gets or sets the associated <see cref="Form"/>.
        /// </summary>
        public Form Form { get; set; } = null!;
    }
}
