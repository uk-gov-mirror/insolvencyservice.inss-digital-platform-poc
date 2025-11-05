using System.ComponentModel.DataAnnotations;

namespace INSS.Web.Components.Models;

public class AddressModel
{
    [Required(ErrorMessage = "Enter address line 1")]
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }
 
    [Required(ErrorMessage = "Enter town or city")]
    public string TownCity { get; set; } = string.Empty;
 
    public string? County { get; set; }
 
    [Required(ErrorMessage = "Enter postcode")]
    public string Postcode { get; set; } = string.Empty;
}