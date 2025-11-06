namespace INSS.Web.Components.Models;

public class FormModel : BaseModel
{
    public SectionModel[] Sections { get; init; } = [];
    
    public Navigation Path { get; init; } = Navigation.Default;
    
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