namespace INSS.Web.Components.Models;

/// <summary>
/// Below is an example of how we can create a whole section upfront and share it with other projects.
///
/// We can a set of pre-built sections that can be easily attached to a form with all the pages defined.
/// </summary>
public static class PrefabModelSections
{
    public static readonly SectionModel YourDetails = new()
    {
        Name = "Your Details",
        PathName = "your-details",
        Pages = [new AddressModel(), new BankAccountModel()]
    };
}