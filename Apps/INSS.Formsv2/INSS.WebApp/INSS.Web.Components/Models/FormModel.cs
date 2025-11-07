namespace INSS.Web.Components.Models;

public class FormModel : BaseModel
{
    private readonly List<Navigation> _navList = new();
    
    public SectionModel[] Sections { get; init; } = [];
    
    public Navigation Path { get; init; } = Navigation.Default;

    public Navigation[] NavigationHistory => _navList.ToArray();

    public string PathName { get; init; } = "form";
    
    public void AddNavigation(Navigation nav)
    {
        _navList.Add(nav);
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
        
        // TODO: Could validate all paths are unique and throw exception if not
    }
}