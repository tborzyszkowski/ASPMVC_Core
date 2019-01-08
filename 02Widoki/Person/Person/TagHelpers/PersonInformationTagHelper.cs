using System;
using Person.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Person.TagHelpers
{
    [HtmlTargetElement("human-information")]
    public class HumanInformationTagHelper : TagHelper
    {
        public Human Info { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Content.SetHtmlContent(
                    $@"<ul><li><strong>Human name:</strong> {Info.Name}</li>
                           <li><strong>Human YOB:</strong> {Info.YoB}</li>
                    </ul>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
