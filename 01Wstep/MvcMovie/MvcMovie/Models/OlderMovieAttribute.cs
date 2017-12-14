using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models {
    public class OlderMovieAttribute : ValidationAttribute {
        private int _year;

        public OlderMovieAttribute(int Year) {
            _year = Year;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Movie movie = (Movie)validationContext.ObjectInstance;

            if (movie.ReleaseDate.Year > _year) {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage() {
            return $"Movies must be older than {_year}.";
        }
    }
}
