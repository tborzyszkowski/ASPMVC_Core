using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace proj4.Models {
    public class BandListener {
        //public int BandListenerID { get; set; }
        public int ListenerID { get; set; }
        public string BandID { get; set; }
        [Range(1, 5)]
        public int Note { get; set; }

        public Listener Listener { get; set; }
        public Band Band { get; set; }
    }
}
