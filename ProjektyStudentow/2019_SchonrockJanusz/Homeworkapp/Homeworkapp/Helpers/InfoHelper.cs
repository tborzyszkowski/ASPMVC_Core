using Homeworkapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Helpers
{
    public static class InfoHelper
    {
        public static MvcHtmlString InfoKolokwium(this HtmlHelper html, List<Kolokwium> kolokwia)
        {
            var divRow = new TagBuilder("div");
            divRow.AddCssClass("row");

            foreach (var kolokwium in kolokwia)
            {
                var divBox = new TagBuilder("div");
                divBox.AddCssClass("box");

                var divInside = new TagBuilder("div");
                divInside.AddCssClass("inside-box effect info");

                var h3 = new TagBuilder("h3");
                var termin = kolokwium.Termin.ToShortDateString();
                h3.SetInnerText(termin);

                var h4 = new TagBuilder("h4");
                var przedmiot = kolokwium.Przedmiot.Nazwa.ToString();
                h4.SetInnerText(przedmiot);

                var h5 = new TagBuilder("h5");
                try
                {
                    var opis = kolokwium.Opis.ToString();
                    h5.SetInnerText(opis);
                }
                catch
                {
                    var opis = "";
                    h5.SetInnerText(opis);
                }

                var p = new TagBuilder("p");

                var a = new TagBuilder("a");
                a.AddCssClass("link");
                try
                {
                    var url = kolokwium.Url.ToString();
                    a.MergeAttribute("href", url);
                    a.MergeAttribute("target", "_blank");
                    a.SetInnerText(url);
                }
                catch
                {
                    var url = "";
                    a.MergeAttribute("href", url);
                    a.MergeAttribute("target", "_blank");
                    a.SetInnerText(url);
                }

                p.InnerHtml += a;
                divInside.InnerHtml += h3.ToString() + h4.ToString() + h5.ToString() + p.ToString();
                divBox.InnerHtml += divInside.ToString();
                divRow.InnerHtml += divBox.ToString();


            }
            return new MvcHtmlString(divRow.ToString());
        }

        public static MvcHtmlString InfoEgzamin(this HtmlHelper html, List<Egzamin> egzaminy)
        {
            var divRow = new TagBuilder("div");
            divRow.AddCssClass("row");

            foreach (var egzamin in egzaminy)
            {
                var divBox = new TagBuilder("div");
                divBox.AddCssClass("box");

                var divInside = new TagBuilder("div");
                divInside.AddCssClass("inside-box effect info");

                var h3 = new TagBuilder("h3");
                var termin = egzamin.Termin.ToShortDateString();
                h3.SetInnerText(termin);

                var h4 = new TagBuilder("h4");
                var przedmiot = egzamin.Przedmiot.Nazwa.ToString();
                h4.SetInnerText(przedmiot);

                var h5 = new TagBuilder("h5");
                try
                {
                    var opis = egzamin.Opis.ToString();
                    h5.SetInnerText(opis);
                }
                catch
                {
                    var opis = "";
                    h5.SetInnerText(opis);
                }

                var p = new TagBuilder("p");

                var a = new TagBuilder("a");
                a.AddCssClass("link");
                try
                {
                    var url = egzamin.Url.ToString();
                    a.MergeAttribute("href", url);
                    a.MergeAttribute("target", "_blank");
                    a.SetInnerText(url);
                }
                catch
                {
                    var url = "";
                    a.MergeAttribute("href", url);
                    a.MergeAttribute("target", "_blank");
                    a.SetInnerText(url);
                }

                p.InnerHtml += a;
                divInside.InnerHtml += h3.ToString() + h4.ToString() + h5.ToString() + p.ToString();
                divBox.InnerHtml += divInside.ToString();
                divRow.InnerHtml += divBox.ToString();


            }
            return new MvcHtmlString(divRow.ToString());
        }
    }
}