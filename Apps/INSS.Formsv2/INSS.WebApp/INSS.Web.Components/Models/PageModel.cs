using System.Reflection;

namespace INSS.Web.Components.Models;

//[JsonPolymorphic]
//[JsonDerivedType(typeof(AddressModel), "Address")]
//[JsonDerivedType(typeof(BankAccountModel), "BackAccount")]
public abstract class PageModel : BaseModel
{
    public bool IsComplete { get; set; }

    public string Title { get; set; } = "";
    
    public string PageUrl { get; set; } = string.Empty;
    
    public string NextPageUrl { get; set; } = string.Empty;
    
    public string PreviousPageUrl { get; set; } = string.Empty;
    
    public string PathName { get; init; } = "page";

    public string[] GetValues()
    {
        var list = new List<string>();

        var ignore = PropertiesToIgnore();

        foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (ignore.Contains(property.Name)) continue;
            
            var value = property.GetValue(this, null);

            if (value is not null)
            {
                list.Add(value.ToString());
            }
        }

        return list.ToArray();
    }

    protected virtual string[] PropertiesToIgnore()
    {
        return [
            nameof(Id),
            nameof(Title),
            nameof(IsComplete),
            nameof(PageUrl),
            nameof(NextPageUrl),
            nameof(PreviousPageUrl),
            nameof(PathName),
            nameof(Controller),
            nameof(Action)
        ];
    }
}