using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class CartResultItem
    {
        // Wholesaler ID
        public int WholesalerId { get; set; }

        // Wholesaler 
        public Wholesaler Wholesaler { get; set; }

        // List of Products
        public ICollection<WholesalerProduct> Products { get; set; }

        // All products available bool
        public bool AllProductsAvail { get; set; }
        // % of products available
        public decimal ProductsAvailPct { get; set; }
        // All products in stock bool
        public bool AllProductsInStock { get; set; }
        // % of products in stock
        public decimal ProductsInStockPct { get; set; }
        // Count of products available
        public int ProductsAvailCount { get; set; }
        // Count of products in stock
        public int ProductsInStockCount { get; set; }

        // Price
        public Decimal Price { get; set; }
    }
}
