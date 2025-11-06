namespace INSS.Web.Components.Models;

public class FormModel : BaseModel
{
    public SectionModel[] Sections { get; init; } = [];
    
    public Navigation Path { get; init; } = Navigation.Default;
    
    public PageModel FindPage(string pageId)
    {
        var page = Sections.SelectMany(section => section.Pages).FirstOrDefault(page => page.Id == pageId);

        return page ?? throw new Exception("Unable to find the page!"); // TODO: Better error
    }

    public SectionModel FindSectionForQuestion(string questionId)
    {
        foreach (var section in Sections)
        {
            if (section.Pages.Any(p => p.Question.Id == questionId))
            {
                return section;
            }
        }
        
        throw new Exception("Unable to find the section for question"); // TODO: Better error
    }
}