using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homeworkapp.Models
{
    public class Przedmiot
    {
        [Key]
        public int PrzedmiotID { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwę przedmiotu")]
        [StringLength(60)]
        public string Nazwa { get; set; }
    }
}