using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class WholesalerProduct:Product
    {
        // Price
        public Decimal Price { get; set; }

        // Sells Product Bool
        public bool SellsProduct { get; set; }

        // In Stock
        public bool InStock { get; set; }
    }
}
