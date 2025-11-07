using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace INSS.Web.Components.Models;

public abstract class PageModel : BaseModel
{
    public bool IsComplete { get; set; }

    public string Title { get; set; } = "";
    
    public Navigation Path { get; set; } = Navigation.Default; // TODO: Why is this needed? records anyhow
    
    public Navigation Next { get; set; } = Navigation.Default;
    
    public Navigation? Previous { get; set; }
    
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
            nameof(Path),
            nameof(Next),
            nameof(Previous),
            nameof(PathName)
        ];
    }
}