using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualBasic;

namespace INSS.Forms.Runner.MVC.TagHelpers
{
    [HtmlTargetElement("inss-task-list")]
    public class TaskListTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public required ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "govuk-task-list");

            var childContent = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(childContent);
        }
    }

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
}
