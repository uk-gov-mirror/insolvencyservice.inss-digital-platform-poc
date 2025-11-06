namespace INSS.Web.Components.Models;

public abstract class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public required Navigation Path { get; set; } = Navigation.Default; // TODO: Why is this needed? records anyhow
    
    public Navigation Next { get; set; } = Navigation.Default;
    
    public Navigation? Previous { get; set; }
}