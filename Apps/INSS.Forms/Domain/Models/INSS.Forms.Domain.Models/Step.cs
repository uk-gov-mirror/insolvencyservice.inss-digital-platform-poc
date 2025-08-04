using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models
{
    /// <summary>
    /// Represents a step in a form workflow.
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Gets or sets the unique identifier for the step.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the step.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the step.
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of forms associated with this step.
        /// </summary>
        public ICollection<StepForm> StepForms { get; set; } = [];
    }
}
