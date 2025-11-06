namespace INSS.Web.Components.Models;

public sealed class SectionModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public PageModel[] Pages { get; set; } = [];

    public Navigation FirstQuestion => Pages.First().Path;
    
    public string FirstQuestionId => Pages.First().Question.Id;

    public PageModel? GetNextPageAfterQuestion(string questionId)
    {
        for (var i = 0; i < Pages.Length; i++)
        {
            if (Pages[i].Question.Id == questionId && i < Pages.Length - 1)
            {
                return Pages[i + 1];
            }
        }

        return null;
    }
}