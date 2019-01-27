using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiMarket.Models
{
    public class OrderItem
    {
        public int ID { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        // ilosc razy cena produktu
        public double TotalPrice
        {
            get { return this.Amount * Product.Price; }
        }

    }
}
