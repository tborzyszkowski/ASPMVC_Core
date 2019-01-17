using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Homeworkapp.Models
{
    public class Zadanie
    {
        [Key]
        public int ZadanieID { get; set; }

        [Display(Name = "Zjazd")]
        public int ZjazdID { get; set; }

        [ForeignKey("ZjazdID")]
        public virtual Zjazd Zjazd { get; set; }

        [Display(Name = "Przedmiot")]
        public int PrzedmiotID { get; set; }

        [ForeignKey("PrzedmiotID")]
        public virtual Przedmiot Przedmiot { get; set; }

        [Required(ErrorMessage = "Podaj opis")]
        public string Opis { get; set; }

        [Url]
        public string Url { get; set; }

        [Required(ErrorMessage = "Podaj termin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Termin { get; set; }
    }
}