using Microsoft.AspNetCore.Razor.TagHelpers;

namespace INSS.Web.Components.TagHelpers;

[HtmlTargetElement("inss-task-list-item")]
public class  TaskListItemTagHelper : TagHelper
{
    public string Name  { get; set; }
    public bool IsComplete { get; set; }
    public string Url { get; set; } 

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", "govuk-task-list__item govuk-task-list__item--with-link");

        output.Content.SetHtmlContent($@"
            <div class=""govuk-task-list__name-and-hint"">
                <a class=""govuk-link govuk-task-list__link"" href=""{Url}"" aria-describedby=""{Name.ToLower().Replace(" ", "-")}-status"">
                    {Name}
                </a>
            </div>
            <div class=""govuk-task-list__status"" id=""{Name.ToLower().Replace(" ", "-")}-status"">
                {(IsComplete ? "Completed" : "<strong class=\"govuk-tag govuk-tag--blue\">Incomplete</strong>")}
            </div>");
    }
}