namespace INSS.Web.Components.Models;

public sealed class Navigation
{
    public static readonly Navigation Default = new() { Controller = "Home", Action = "Index" };
    
    public required string Controller { get; init; }

    public string Action { get; init; } = "Index";

    public string TempUrl { get; set; } = string.Empty; // TODO: Fix
}