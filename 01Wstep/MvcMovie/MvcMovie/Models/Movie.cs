using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie : IValidatableObject {
        private const int _expensive = 1000;
        private const int _oldYear = 1960;

        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Remote(
            action: "VerifyTitle", 
            controller: "Movies",
            AdditionalFields = nameof(ReleaseDate)
            )]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [OlderMovie(1960)]
        [Remote(
            action: "VerifyTitle",
            controller: "Movies",
            AdditionalFields = nameof(Title)
            )]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        [Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Remote(action: "VerifyRating", controller: "Movies", ErrorMessage = "Use a new rating")]
        public string Rating { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (Price > _expensive && ReleaseDate.Year <= _oldYear) {
                yield return new ValidationResult(
                    $"Old movies are not so expensive: {_expensive}.",
                    new[] { "ReleaseDate", "Price" });
            }
        }
    }
}
