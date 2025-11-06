namespace INSS.Web.Components.Models;

public abstract class BaseQuestionModel : BaseModel
{
    public Navigation Back { get; set; } = Navigation.Default;
}