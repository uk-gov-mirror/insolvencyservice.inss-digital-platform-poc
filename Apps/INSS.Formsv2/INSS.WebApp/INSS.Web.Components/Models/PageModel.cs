namespace INSS.Web.Components.Models;

public sealed class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public Navigation Path { get; init; } = Navigation.Default;
    
    public BaseQuestionModel Question { get; init; } = null!;
}