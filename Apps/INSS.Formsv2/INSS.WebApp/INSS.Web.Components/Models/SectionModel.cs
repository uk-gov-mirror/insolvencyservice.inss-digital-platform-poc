namespace INSS.Web.Components.Models;

public sealed class SectionModel : BaseModel
{
    private readonly List<PageModel> _pages = [];
    
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public PageModel[] Pages => _pages.ToArray();
    
    public string PathName { get; init; } = "Section";
    
    public string PageUrl { get; set; } = string.Empty;

    public void AddPage(PageModel page)
    {
        page.PageUrl = $"{PageUrl}/{page.PathName}";
        _pages.Add(page);
    }
    
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
    
    public bool IsLastPageInSection(PageModel page)
    {
        return Pages.Last().Id == page.Id;   
    }
}