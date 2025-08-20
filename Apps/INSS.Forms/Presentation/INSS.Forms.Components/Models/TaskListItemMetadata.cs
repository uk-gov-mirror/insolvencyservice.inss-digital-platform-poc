namespace INSS.Forms.Components.Models
{
    public class TaskListItemMetadata : TaskListItem
    {
        public Guid FormId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
