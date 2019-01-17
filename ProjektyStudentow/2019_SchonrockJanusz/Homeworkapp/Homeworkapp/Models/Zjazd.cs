using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homeworkapp.Models
{
    public class Zjazd
    {
        [Key]
        public int ZjazdID { get; set; }

        [Required(ErrorMessage = "Wprowadź numer zjazdu")]
        [StringLength(3)]
        public string Numer { get; set; }

        [Required(ErrorMessage = "Wprowadź datę")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Dzien1 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Dzien2 { get; set; }
    }
}