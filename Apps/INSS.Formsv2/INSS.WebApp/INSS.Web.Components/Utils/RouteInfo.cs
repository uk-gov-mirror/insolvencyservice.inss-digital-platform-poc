namespace INSS.Web.Components.Utils;

public sealed class RouteInfo
{
    public const string CookieName = "RouteId";
    
    public required string Url { get; init; }
    
    public required string Id { get; init; }
}