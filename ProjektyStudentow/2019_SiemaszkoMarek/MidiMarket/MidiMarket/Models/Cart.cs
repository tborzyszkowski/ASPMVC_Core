using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MidiMarket.Models
{
    /// Koszyk: Listing 8.12
    public class Cart
    {
        /// <summary>
        /// Kolekcja wszystkich pozycji koszyka
        /// </summary>
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Dodaje elementu do koszuka
        /// </summary>
        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                            .Where(p => p.Product.ID == product.ID)
                            .FirstOrDefault();

            // jezeli w koszyku nie ma jeszcze takiego produktu to go wlozy
            if (line == null)
            {
                lineCollection.Add( new CartLine { Product = product, Quantity = quantity });
            }
            // w przeciwnym razie tylko zwiekszy ilosc
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Usuwa element z koszyka
        /// </summary>
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll( l => l.Product.ID == product.ID);
        }

        /// <summary>
        /// Obliczenie całkowitej wartości towarów
        /// </summary>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => (decimal) e.Product.Price * e.Quantity);
        }

        /// <summary>
        /// Wyczysc koszyk
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        /// <summary>
        /// Pobiera pozycje zamowienia
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get {
                return lineCollection;
            }
        }
    }
    
    /// <summary>
    /// reprezentuje jedną pozcje w koszuku produkt x ilość
    /// </summary>
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}