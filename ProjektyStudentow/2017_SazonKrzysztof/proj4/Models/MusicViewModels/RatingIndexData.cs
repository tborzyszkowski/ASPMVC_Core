using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj4.Models.MusicViewModels {
    public class RatingIndexData {
        public IEnumerable<Listener> Listeners { get; set; }
        public IEnumerable<Band> Bands { get; set; }
        public IEnumerable<BandListener> BandsListeners { get; set; }
        //?
        public IEnumerable<Tour> Tours { get; set; }
    }
}
