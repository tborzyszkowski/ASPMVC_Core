using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Homeworkapp.Models
{
    public class Kolokwium
    {
        [Key]
        public int KolokwiumID { get; set; }

        [Display(Name = "Przedmiot")]
        public int PrzedmiotID { get; set; }

        [ForeignKey("PrzedmiotID")]
        public virtual Przedmiot Przedmiot { get; set; }

        [Required(ErrorMessage = "Podaj termin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Termin { get; set; }


        public string Opis { get; set; }

        [Url]
        public string Url { get; set; }
    }
}