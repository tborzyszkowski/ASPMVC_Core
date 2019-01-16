using Homeworkapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Helpers
{
    public static class ZjazdZadanieHelper
    {
        public static MvcHtmlString ListaZadan(this HtmlHelper html, List<Zadanie> listaZadan)
        {

            TagBuilder divRow = new TagBuilder("div");
            divRow.AddCssClass("row");

            foreach (var zadanie in listaZadan)
            {
                var divTagBox = new TagBuilder("div"); // <div>
                divTagBox.AddCssClass("box");

                    var divTagInside = new TagBuilder("div"); // <a>
                    divTagInside.AddCssClass("inside-box effect");

                        var divTagSubject = new TagBuilder("div");
                        divTagSubject.AddCssClass("subject");

                            var h4Tag = new TagBuilder("h4");
                            var nazwaPrzedmiotu = zadanie.Przedmiot.Nazwa.ToString();
                            h4Tag.SetInnerText(nazwaPrzedmiotu);

                        divTagSubject.InnerHtml += h4Tag.ToString();
                    divTagInside.InnerHtml += divTagSubject.ToString();

                        var h5Tag = new TagBuilder("h5");
                        var opisZadania = zadanie.Opis.ToString();
                        h5Tag.SetInnerText(opisZadania);

                    divTagInside.InnerHtml += h5Tag.ToString();


                        var pTag = new TagBuilder("p");

                            var aTag = new TagBuilder("a");
                            aTag.AddCssClass("link");
                            try
                            {
                                var url = zadanie.Url.ToString();
                                aTag.MergeAttribute("href", url);
                                aTag.MergeAttribute("target", "_blank");
                                aTag.SetInnerText(url);
                            }
                            catch
                            {
                                var url = "";
                                aTag.MergeAttribute("href", url);
                                aTag.MergeAttribute("target", "_blank");
                                aTag.SetInnerText(url);
                            }
                            //var url = zadanie.Url.ToString();
                            
                        pTag.InnerHtml += aTag.ToString();

                    divTagInside.InnerHtml += pTag.ToString();

                        var divTagDeadline = new TagBuilder("div");
                        divTagDeadline.AddCssClass("deadline");

                            var h6Tag = new TagBuilder("h6");
                            var termin = "Termin: " + zadanie.Termin.ToShortDateString();
                            h6Tag.SetInnerText(termin);

                        divTagDeadline.InnerHtml += h6Tag.ToString();
                    divTagInside.InnerHtml += divTagDeadline.ToString();
                divTagBox.InnerHtml += divTagInside.ToString();
             divRow.InnerHtml += divTagBox.ToString();





                ////var url = "/ListaZjazdow/Details/" + zjazd.ZjazdID.ToString();
                //aTag.MergeAttribute("href", url);

                //var divTag = new TagBuilder("div"); // <div>
                //divTag.AddCssClass("inside effect");

                //var h4Tag = new TagBuilder("h4"); // <h4></h4>
                //var NapisZjazd = "Zjazd " + zjazd.Numer.ToString();
                //h4Tag.SetInnerText(NapisZjazd);

                //var h2Tag = new TagBuilder("h2"); // <h2></h2>
                //var NapisData = zjazd.Dzien1.Day.ToString() + "/" + zjazd.Dzien2.Day.ToString();
                //h2Tag.SetInnerText(NapisData);

                //var h3Tag = new TagBuilder("h3"); // <h3></h3>
                //var NapisMiesiac = zjazd.Dzien1.ToString("MMMM");
                //h3Tag.SetInnerText(NapisMiesiac);

                //divTag.InnerHtml += h4Tag.ToString();
                //divTag.InnerHtml += h2Tag.ToString();
                //divTag.InnerHtml += h3Tag.ToString(); // </div>

                //aTag.InnerHtml += divTag.ToString(); // </a>

                //divTagBox.InnerHtml += aTag.ToString(); // </div>

                //divRow.InnerHtml += divTagBox.ToString();

            }


            return new MvcHtmlString(divRow.ToString());
        }

     
    }
}