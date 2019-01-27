using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MidiMarket.Models;

namespace MidiMarket.Helpers
{
    // Listing 7.18
    public static class PagingHelpers
    {
        static TagBuilder generateButton(string text, string pageUrl, bool selected = false)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl);
            tag.InnerHtml = text;
            if (selected)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            tag.MergeAttribute("style", "margin-left: 5px");
            return tag;
        }

        public static MvcHtmlString PageLinks(  this HtmlHelper html, 
                                                PagingInfo pagingInfo, 
                                                Func<int, string> pageUrl
                                              )
        {
            int currentPage = pagingInfo.CurrentPage;
            int totalPage = pagingInfo.TotalPages;
            StringBuilder result = new StringBuilder();

            // button wstecz
            if (totalPage > 1)
            {
                int prevPage = Math.Max(currentPage - 1, 1);
                TagBuilder button = generateButton("«", pageUrl(prevPage));
                result.Append(button.ToString());
            }

            // buttony stron
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder button = generateButton(i.ToString(), pageUrl(i), i == pagingInfo.CurrentPage);
                result.Append(button.ToString());
            }

            // button dalej
            if (totalPage > 1)
            {
                int nextPage = Math.Min(currentPage + 1, totalPage);
                TagBuilder button = generateButton("»", pageUrl(nextPage));
                result.Append(button.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    } 
}
