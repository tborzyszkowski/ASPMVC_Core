using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace proj4.Models.MusicViewModels {
    public class BandGenreGroup {

        [DataType(DataType.Duration)]
        public Genre? Genre { get; set; }

        public int GenreCount { get; set; }
    }
}
