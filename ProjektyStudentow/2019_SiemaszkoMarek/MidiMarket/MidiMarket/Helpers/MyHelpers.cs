using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MidiMarket.Helpers
{
    public static class MyHelpers
    {
        public static string imagePath = @"/Content/Images/";
        public static string iconPath = @"/Content/Icons/";

        public enum Icon
        {
            iconDelete,
            iconNew
        }

        static Dictionary<Icon, string> IconSrcs = new Dictionary<Icon, string>
        {
            {Icon.iconDelete, "ico_delete.png"},
            {Icon.iconNew, "ico_add.png"}
        };


        public static MvcHtmlString WstawMiniaturke(this HtmlHelper htmlHelper, string imageSrc, object htmlAttributes = null)
        {
            var img = new TagBuilder("img");
            img.MergeAttribute("src", imagePath + imageSrc);
            img.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString WstawIkone(this HtmlHelper htmlHelper, string action, string controller, Icon icon, object routeValues, object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", iconPath + IconSrcs[icon]);

            var anchor = new TagBuilder("a");
            anchor.InnerHtml = img.ToString(TagRenderMode.SelfClosing);
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(anchor.ToString());
        }

        public static MvcHtmlString WstawForm(this HtmlHelper htmlHelper, Func<object, string> funkc )
        {
            var div = new TagBuilder("div");
            //var label = new TagBuilder("label");
            //label.MergeAttribute("for", funkc );

            //<div class="form-group">
            //    @Html.LabelFor(x => x.Name, htmlAttributes: new { @class = "" })
            //    @Html.EditorFor(x => x.Name, new { htmlAttributes = new { @class = "form-control" } })
            //    @Html.ValidationMessageFor(x => x.Name, "", new { @class = "text-danger" })
            //</div>
            return new MvcHtmlString(div.ToString());
        }

    }
}