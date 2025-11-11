using System.ComponentModel.DataAnnotations;

namespace INSS.Web.Components.Models;

public sealed class FullNameModel : PageModel
{
    public FullNameModel()
    {
        PathName = "full-name";
        Title = "Full Name";
        Controller = "FullName";
    }

    [Required(ErrorMessage = "Enter your full name")]
    [RegularExpression("^[A-Za-z\\s\\-']+$", ErrorMessage = "Full name can only contain letters, spaces, hyphens and apostrophes")]
    public string FullName { get; set; }
}