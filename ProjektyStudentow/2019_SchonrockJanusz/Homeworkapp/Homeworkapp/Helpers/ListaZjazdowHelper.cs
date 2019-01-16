using Homeworkapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Helpers
{
    public static class ListaZjazdowHelper
    {
        //public static MvcHtmlString Powitanie(this HtmlHelper html, string slowo)
        //{
        //    TagBuilder tag = new TagBuilder("h2");

        //    tag.SetInnerText(slowo);

        //    return new MvcHtmlString(tag.ToString());
        //}

        public static MvcHtmlString PokazDate(this HtmlHelper html, List<Zjazd> zjazd)
        {
            TagBuilder h2Tag = new TagBuilder("h2");
            h2Tag.AddCssClass("text-center");
            foreach (var dzien in zjazd)
            {
                var dzien1 = dzien.Dzien1.Day;
                var dzien2 = dzien.Dzien2.Day;
                var miesiac = dzien.Dzien1.ToString("MMMM");
                var data = dzien1 + "/" + dzien2 + " " + miesiac;
                h2Tag.SetInnerText(data.ToString());
            }
            return new MvcHtmlString(h2Tag.ToString());
        }

        public static MvcHtmlString ListaZjazdow(this HtmlHelper html, List<Zjazd> listaZjazdow)
            {
            TagBuilder divRow = new TagBuilder("div");
            divRow.AddCssClass("row");

            foreach (var zjazd in listaZjazdow)
            {
                var divTagBox = new TagBuilder("div"); // <div>
                divTagBox.AddCssClass("box");
                var aTag = new TagBuilder("a"); // <a>
                var url = "/ListaZjazdow/Details/" + zjazd.ZjazdID.ToString();
                aTag.MergeAttribute("href", url);

                var divTag = new TagBuilder("div"); // <div>
                divTag.AddCssClass("inside effect");

                var h4Tag = new TagBuilder("h4"); // <h4></h4>
                var NapisZjazd = "Zjazd " + zjazd.Numer.ToString();
                h4Tag.SetInnerText(NapisZjazd);

                var h2Tag = new TagBuilder("h2"); // <h2></h2>
                var NapisData = zjazd.Dzien1.Day.ToString() + "/" + zjazd.Dzien2.Day.ToString();
                h2Tag.SetInnerText(NapisData);

                var h3Tag = new TagBuilder("h3"); // <h3></h3>
                var NapisMiesiac = zjazd.Dzien1.ToString("MMMM");
                h3Tag.SetInnerText(NapisMiesiac);

                divTag.InnerHtml += h4Tag.ToString(); 
                divTag.InnerHtml += h2Tag.ToString();
                divTag.InnerHtml += h3Tag.ToString(); // </div>

                aTag.InnerHtml += divTag.ToString(); // </a>

                divTagBox.InnerHtml += aTag.ToString(); // </div>

                divRow.InnerHtml += divTagBox.ToString();

            }
     

            return new MvcHtmlString(divRow.ToString());
        }

        
    }
}