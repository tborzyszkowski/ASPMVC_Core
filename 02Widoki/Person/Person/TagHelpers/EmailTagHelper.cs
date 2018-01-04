using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Person.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "ug.edu.pl";

        // Can be passed via <email mail-to="..." />. 
        // Pascal case gets translated into lower-kebab-case.
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag

            var address = MailTo + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}

