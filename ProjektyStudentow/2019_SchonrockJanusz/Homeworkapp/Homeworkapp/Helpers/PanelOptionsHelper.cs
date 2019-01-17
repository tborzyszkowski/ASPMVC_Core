using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Helpers
{
    public static class PanelOptionsHelper
    {
        public static MvcHtmlString PokazOpcje(this HtmlHelper html, string nazwa, string methodName, string controllerName)
        {

            var divBox = new TagBuilder("div"); // <div>
            divBox.AddCssClass("box");
            var a = new TagBuilder("a");
            a.AddCssClass("link");
            var url = "";
            if (methodName == "index" || methodName == "Index")
            {
                url = controllerName;
            }
            else
            {
                url = controllerName + "/" + methodName;
            }

            a.MergeAttribute("href", url);

            var divInside = new TagBuilder("div"); // <div>
            divInside.AddCssClass("inside effect");

            var h4Tag = new TagBuilder("h4"); // <h4></h4>
            h4Tag.SetInnerText(nazwa);

            divInside.InnerHtml += h4Tag.ToString();

            a.InnerHtml += divInside.ToString(); // </a>

            divBox.InnerHtml += a.ToString(); // </div>

            return new MvcHtmlString(divBox.ToString());
        }

        public static MvcHtmlString PokazOpcje(this HtmlHelper html, string nazwa, string methodName, string controllerName, int iloscWystapien)
        {
            var divBox = new TagBuilder("div"); // <div>
            divBox.AddCssClass("box");
            var a = new TagBuilder("a");
            a.AddCssClass("link");
            var url = "";
            if (methodName == "index" || methodName == "Index")
            {
                url = controllerName;
            }
            else
            {
                url = controllerName + "/" + methodName;
            }

            a.MergeAttribute("href", url);

            var divInside = new TagBuilder("div"); // <div>
            divInside.AddCssClass("inside effect");

            var h4Tag = new TagBuilder("h4"); // <h4></h4>
            h4Tag.SetInnerText(nazwa);

            var h2Tag = new TagBuilder("h2");
            h2Tag.SetInnerText(iloscWystapien.ToString());

            divInside.InnerHtml = h4Tag.ToString() + h2Tag.ToString();

            a.InnerHtml += divInside.ToString(); // </a>

            divBox.InnerHtml += a.ToString(); // </div>

            return new MvcHtmlString(divBox.ToString());
        }
    }
}