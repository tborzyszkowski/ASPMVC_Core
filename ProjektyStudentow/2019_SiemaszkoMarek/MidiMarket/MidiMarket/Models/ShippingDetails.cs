using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidiMarket.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Proszę podać imie i nazwisko.")]
        [Display(Name = "Imie Nazwisko")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę wpisać ulicę i numer domu")]
        [Display(Name = "Ulica i numer domu")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę miasta.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        //todo: regexp kod pocztowy
        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Proszę uzupełnić kod pocztowy")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kraju.")]
        [Display(Name = "Kraj")]
        public string Country { get; set; }

    }
}