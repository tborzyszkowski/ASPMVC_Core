using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MidiMarket.Models
{
    // Przekazywanie danych do widoku koszyka: Listing 8.15
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
