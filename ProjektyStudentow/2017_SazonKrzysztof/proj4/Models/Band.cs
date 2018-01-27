using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;

namespace proj4.Models {
    public enum Genre {
        folk, art, pop
    }

    public class Band {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Band name")]
        [Remote(action: "VerifyBandID", controller: "Bands", AdditionalFields = nameof(City))]
        public string BandID { get; set; }

        [Required]
        [Display(Name = "City of origin")]
        [RegularExpression(@"^(?:\b[A-Z][a-z]+\b ?)+$")]
        [Remote(action: "VerifyBandID", controller: "Bands", AdditionalFields = nameof(BandID))]
        public string City { get; set; }

        [Display(Name = "Formation date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FormationDate { get; set; }

        [GenreValidation]
        [DisplayFormat(NullDisplayText = "Genre not set")]
        public Genre? Genre { get; set; }

        [Display(Name = "Band listeners")]
        public ICollection<BandListener> BandsListeners { get; set; }
        public ICollection<Tour> Tours { get; set; }
        //?
        //public ICollection<Listener> Listeners { get; set; }

        public class GenreValidation : ValidationAttribute {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
                var model = validationContext.ObjectInstance as Band;

                if (model.FormationDate < DateTime.Parse("1900-01-01") && model.Genre == Models.Genre.pop)
                    return new ValidationResult(GetErrorMessage(validationContext));

                return ValidationResult.Success;
            }

            private string GetErrorMessage(ValidationContext validationContext) {
                if (!string.IsNullOrEmpty(this.ErrorMessage))
                    return this.ErrorMessage;

                return $"Pop as known today didn't exist that early";
            }
        }
    }
}
