using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiMarket.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public int ID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        public string ShipingName { get; set; }
        public string ShipingAddress { get; set; }
        public string ShipingCity { get; set; }
        public string ShipingZip { get; set; }
        public string ShipingCountry { get; set; }

        // virtual
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
