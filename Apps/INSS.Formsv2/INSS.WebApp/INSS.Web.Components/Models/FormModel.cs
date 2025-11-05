namespace INSS.Web.Components.Models;

public abstract class BaseModel
{
    public string Id { get; init; } = string.Empty;
}

public class FormModel : BaseModel
{
    public SectionModel[] Sections { get; set; } = [];
}

public sealed class SectionModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public string Url { get; set; } = "/bankaccount"; // TODO: Will an action from the journey
    
    public PageModel[] Pages { get; set; } = [];
}

public sealed class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public BaseModel[] Questions { get; set; } = [];
}