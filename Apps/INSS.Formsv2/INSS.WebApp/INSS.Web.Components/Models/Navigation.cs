namespace INSS.Web.Components.Models;

public sealed class Navigation
{
    public static readonly Navigation Default = new Navigation { Controller = "Home", Action = "Index", Id = null };
    
    public required string? Id { get; init; }
    
    public required string Controller { get; init; }

    public string Action { get; init; } = "Index";

    public string TempUrl { get; set; } = string.Empty; // TODO: Fix
}