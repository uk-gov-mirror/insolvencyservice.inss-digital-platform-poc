namespace INSS.Web.Components.Models;

public class FormModel : BaseModel
{
    private readonly List<Navigation> _navList = new List<Navigation>();
    
    public SectionModel[] Sections { get; init; } = [];
    
    public Navigation Path { get; init; } = Navigation.Default;

    public Navigation[] NavList => _navList.ToArray();

    public void AddNav(Navigation nav)
    {
        _navList.Add(nav);
    }

    public void PopLastNav()
    {
        if (_navList.Count > 0)
        {
            _navList.Remove(_navList.Last());
        }
    }

    public void PopAllNav()
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
}