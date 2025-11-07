namespace INSS.Web.Components.Models;

public sealed class SectionModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public bool IsComplete => Pages.All(p => p.IsComplete);
    
    public PageModel[] Pages { get; set; } = [];
    
    public string PathName { get; init; } = "Section";
    
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

    public string GetSectionUrl(FormModel form)
    {
        return $"/{form.PathName}/{PathName}";
    }
    
    public string GetPageUrl(FormModel form, PageModel page)
    {
        return $"/{form.PathName}/{PathName}/{page.PathName}";
    }
    
    public bool IsLastPageInSection(PageModel page)
    {
        return Pages.Last().Id == page.Id;   
    }
}