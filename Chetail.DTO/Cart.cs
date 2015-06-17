using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class Cart
    {
        // Retailer ID
        public int RetailerId { get; set; }

        // List of Chosen Wholesalers
        public ICollection<Wholesaler> Wholesalers { get; set; }

        // List of Products
        public ICollection<Product> Products { get; set; }

    }
}
