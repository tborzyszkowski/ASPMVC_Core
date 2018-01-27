using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace proj4.TagHelpers {
    public class NumberTagHelper : TagHelper {
        private string processNum(string preText) {
            string outText = "";
            string num;

            foreach (var c in preText) {
                switch (int.Parse(c.ToString())) {
                    case 0:
                        num = "zero";
                        break;
                    case 1:
                        num = "one";
                        break;
                    case 2:
                        num = "two";
                        break;
                    case 3:
                        num = "three";
                        break;
                    case 4:
                        num = "four";
                        break;
                    case 5:
                        num = "five";
                        break;
                    case 6:
                        num = "six";
                        break;
                    case 7:
                        num = "seven";
                        break;
                    case 8:
                        num = "eight";
                        break;
                    case 9:
                        num = "nine";
                        break;
                    default:
                        num = "NaN";
                        break;
                }

                outText += num;
                outText += " ";
            }
            return outText;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var preText = output.Content.GetContent();
            output.Content.SetContent(processNum(preText));
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            var content = await output.GetChildContentAsync();
            var preText = content.GetContent();
            output.Content.SetContent(processNum(preText));
        }
    }
}
