namespace INSS.Web.Components.Models;

public sealed class PageModel : BaseModel
{
    public bool IsComplete { get; set; }
    
    public BaseQuestionModel[] Questions { get; set; } = [];
}