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

    public PageModel FindPageFromQuestion(string questionId)
    {
        foreach (var section in Sections)
        {
            foreach (var page in section.Pages)
            {
                if (page.Question.Id == questionId)
                {
                    return page;
                }
            }
        }
        
        throw new Exception("Unable to find the page!");
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

    public TQuestion FindQuestion<TQuestion>(string questionId) where TQuestion : BaseQuestionModel
    {
        foreach (var section in Sections)
        {
            foreach (var page in section.Pages)
            {
                if (page.Question.Id == questionId && page.Question is TQuestion questionModel)
                {
                    return questionModel;
                }
            }
        }
        
        throw new Exception("Unable to find question from Id"); // TODO: Better method!
    }
}