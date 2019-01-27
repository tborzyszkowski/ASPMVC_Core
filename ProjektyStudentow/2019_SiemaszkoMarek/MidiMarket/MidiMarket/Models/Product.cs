using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiMarket.Models
{
    public enum Category
    {
        owoce,
        warzywa,
        wypieki,
        napoje,
        zwierzeta,
        zabawki
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public Category? Category { get; set; }
    }
}
