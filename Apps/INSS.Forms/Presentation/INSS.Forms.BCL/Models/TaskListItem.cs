namespace INSS.Forms.BCL.Models
{
    /// <summary>
    /// Represents an item in a task list, including its identifier, title, link, and status.
    /// </summary>
    public class TaskListItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task list item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the task list item.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hyperlink reference associated with the task list item.
        /// </summary>
        public string HRef { get; set; } = "#";

        /// <summary>
        /// Gets or sets the status of the task list item.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
