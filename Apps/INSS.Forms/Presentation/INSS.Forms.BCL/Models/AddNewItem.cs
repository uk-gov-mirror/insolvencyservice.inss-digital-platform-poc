using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.BCL.Models
{
    /// <summary>
    /// Represents a model for adding a new item.
    /// </summary>
    public class AddNewItem
    {
        /// <summary>
        /// Gets or sets the value indicating the new item to add.
        /// </summary>
        [Required(ErrorMessage = "Select 'Yes' if you want to add another income or 'No' if you want to proceed to the summary")]
        public string? AddNew { get; set; }
    }
}
