namespace INSS.Web.Components.Models;

public abstract class BaseModel
{
    public string Id { get; init; } = string.Empty;
    
    public Navigation Url { get; init; } = Navigation.Default;
}