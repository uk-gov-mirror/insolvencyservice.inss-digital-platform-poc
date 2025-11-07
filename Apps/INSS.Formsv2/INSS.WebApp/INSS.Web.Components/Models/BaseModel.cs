namespace INSS.Web.Components.Models;

public abstract class BaseModel
{
    public string Id { get; set; } = string.Empty;
    
    public string Controller { get; set; } = string.Empty;

    public string Action { get; set; } = "Index";
}