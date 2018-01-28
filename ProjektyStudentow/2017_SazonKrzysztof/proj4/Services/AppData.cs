using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj4.Services {
    public class AppData {
        private int mainPageViews;
        public int MainPageViews {
            get {
                return mainPageViews;
            }
            set {
                mainPageViews = value;
            }
        }
    }
}
