namespace INSS.Web.Components.Models;

public sealed class SectionModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public PageModel[] Pages { get; set; } = [];

    public string FirstQuestionId => Pages.First().Questions.First().Id; // TODO: Clean up
}