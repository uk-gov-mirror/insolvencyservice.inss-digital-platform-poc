namespace INSS.Web.Components.Models;

public abstract class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public Navigation Path { get; init; } = Navigation.Default;
    
    public Navigation Next { get; set; } = Navigation.Default;
    
    public Navigation Back { get; set; } = Navigation.Default;
}