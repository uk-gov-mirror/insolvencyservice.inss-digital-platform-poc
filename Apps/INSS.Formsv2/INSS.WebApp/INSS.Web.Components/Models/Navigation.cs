namespace INSS.Web.Components.Models;

public sealed class Navigation
{
    public static readonly Navigation Default = new Navigation { Controller = "Home", Action = "Index" };
    
    public required string Controller { get; init; }

    public string Action { get; init; } = "Index";
}