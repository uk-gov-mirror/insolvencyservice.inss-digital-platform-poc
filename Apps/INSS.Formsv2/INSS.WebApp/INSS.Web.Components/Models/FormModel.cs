using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace INSS.Web.Components.Models;

public class FormModel : BaseModel
{
    private readonly List<string> _navList = [];
    private static JsonSerializerOptions? _options;
    
    public FormModel()
    {
        PathName = "tasks";
        Controller = "Form";
        PageUrl = $"/{PathName}";
    }

    public SectionModel[] Sections { get; set; } = [];
    
    public string PageUrl { get; set; }

    public string[] NavigationHistory => _navList.ToArray();

    public string PathName { get; init; }

    public void AddSection(SectionModel section)
    {
        section.PageUrl = $"/{PathName}/{section.PathName}";
        Sections = Sections.Concat([section]).ToArray();
    }
    
    public void AddNavigation(string url)
    {
        _navList.Add(url);
    }

    public void PopLastNavigationHistory()
    {
        if (_navList.Count > 0)
        {
            _navList.Remove(_navList.Last());
        }
    }

    public void PopAllNavigationHistory()
    {
        _navList.Clear();
    }
    
    public TPage FindPage<TPage>(string pageId) where TPage : PageModel
    {
        foreach (var section in Sections)
        {
            foreach (var page in section.Pages)
            {
                if (page.Id == pageId && page is TPage modelPage)
                {
                    return modelPage;
                }
            }
        }
        
        throw new Exception("Unable to find the page!"); // TODO: Better error
    }

    public SectionModel FindSection(string sectionId)
    {
        var section = Sections.FirstOrDefault(s => s.Id == sectionId);
        
        return section ?? throw new Exception("Unable to find the section!"); // TODO: Better error
    }
    
    public SectionModel FindSectionForPage(string pageId)
    {
        foreach (var section in Sections)
        {
            if (section.Pages.Any(page => page.Id == pageId))
            {
                return section;
            }
        }
        
        throw new Exception("Unable to find the section for page."); // TODO: Better error
    }

    public void Initialize()
    {
        Id = PathName;
        
        foreach (var section in Sections)
        {
            section.Id = $"{PathName}-{section.PathName}";
            
            foreach (var page in section.Pages)
            {
                page.Id = $"{section.Id}-{page.PathName}";
            }
        }

        _options ??= CreateOptions(this);

        // TODO: Could validate all paths are unique and throw exception if not
    }

    public static FormModel Deserialize(string json)
    {
        //var options = CreateOptions(this);
        return JsonSerializer.Deserialize<FormModel>(json, _options);
    }
    public string Serialize()
    {
        return JsonSerializer.Serialize(this, _options);
    }

    private static JsonSerializerOptions CreateOptions(FormModel form)
    {
        var derivedPageModelTypes = new List<JsonDerivedType>();

        foreach (var section in form.Sections)
        {
            foreach (var page in section.Pages)
            {
                if (derivedPageModelTypes.Any(t => 
                        t.TypeDiscriminator?.ToString() == page.GetType().Name))
                {
                    continue;
                }
                
                derivedPageModelTypes.Add(new JsonDerivedType(page.GetType(), page.GetType().Name));
            }
        }
        
        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    typeInfo =>
                    {
                        if (typeInfo.Type == typeof(PageModel))
                        {
                            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = "$type"
                            };

                            foreach (var type in derivedPageModelTypes)
                            {
                                typeInfo.PolymorphismOptions.DerivedTypes.Add(type);
                            }
                        }
                    }
                }
            }
        };

        return options;
    }
}