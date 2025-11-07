using System.ComponentModel.DataAnnotations;
using INSS.Web.Components.Models;

namespace INSS.WebApp.Models;

public class HomeValueModel : PageModel
{
    public HomeValueModel()
    {
        Path = new Navigation { Controller = "HomeValue" };
        PathName = "home-value";
        Title = "Home Value";
    }
    
    [Required(ErrorMessage = "Enter your home value")]
    [Range(100, 1_000_000, ErrorMessage = "The value must between £100 and £1,000,000")]
    public int Value { get; set; }
}