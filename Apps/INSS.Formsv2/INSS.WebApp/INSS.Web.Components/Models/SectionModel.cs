namespace INSS.Web.Components.Models;

public sealed class SectionModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public PageModel[] Pages { get; set; } = [];

    public Navigation FirstPage => Pages.First().Path;
    
    public string FirstPageId => Pages.First().Id;

    public PageModel? GetNextPage(string pageId)
    {
        for (var i = 0; i < Pages.Length; i++)
        {
            if (Pages[i].Id == pageId && i < Pages.Length - 1)
            {
                return Pages[i + 1];
            }
        }

        return null;
    }
}