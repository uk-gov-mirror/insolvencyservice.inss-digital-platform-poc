namespace INSS.Web.Components.Models;

public abstract class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public Navigation Path { get; set; } = Navigation.Default;
    
    public Navigation Next { get; set; } = Navigation.Default;
    
    public Navigation? Previous { get; set; } // = Navigation.Default;
}