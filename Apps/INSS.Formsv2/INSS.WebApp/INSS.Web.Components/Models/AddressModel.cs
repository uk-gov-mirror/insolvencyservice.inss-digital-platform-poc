using System.ComponentModel.DataAnnotations;

namespace INSS.Web.Components.Models;

public class AddressModel : PageModel
{
    public AddressModel()
    {
        PathName = "address";
        Controller = "Address";
    }
    
    [Required(ErrorMessage = "Enter address line 1")]
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }
 
    [Required(ErrorMessage = "Enter town or city")]
    public string TownCity { get; set; } = string.Empty;
 
    public string? County { get; set; }
 
    [Required(ErrorMessage = "Enter postcode")]
    [RegularExpression(
        @"^(GIR ?0AA|(?:(?:[A-PR-UWYZa-pr-uwyz][0-9][0-9]?|[A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][0-9]?|[A-PR-UWYZa-pr-uwyz][0-9][A-HJKSTUWa-hjkstuw]?|[A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][ABEHMNPRVWXYabehmnprvwxy])) ?[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2})$",
        ErrorMessage = "Enter a full UK postcode")]
    public string Postcode { get; set; } = string.Empty;
}