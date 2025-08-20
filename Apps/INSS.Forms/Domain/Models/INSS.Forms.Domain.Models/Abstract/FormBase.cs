using INSS.Forms.Domain.Models.Composite;
using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models.Abstract
{

    /// <summary>
    /// Provides a base class for form models, including common properties such as instance identifier and user.
    /// </summary>
    public abstract class FormBase
    {
        [Required]
        public FormMetadata? FormMetadata { get; set; }
    }
}
