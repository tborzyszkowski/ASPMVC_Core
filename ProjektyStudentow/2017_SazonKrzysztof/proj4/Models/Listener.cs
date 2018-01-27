using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj4.Models {
    public class Listener {
        public int ListenerID { get; set; }

        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 characters")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Column("Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public int Age {
            get {
                if (DateOfBirth == null)
                    return 0;

                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(DateOfBirth.ToString("yyyyMMdd"));
                return (now - dob) / 10000;
            }
        }

        public string OwnerID { get; set; }

        public ListenerStatus Status { get; set; }

        public ICollection<BandListener> BandsListeners { get; set; }
        public ICollection<Band> Bands { get; set; }
    }

    public enum ListenerStatus {
        Submitted,
        Approved,
        Rejected,
        Blocked
    }
}
