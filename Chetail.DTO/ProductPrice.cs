using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class ProductPrice:DTOBase
    {
        // Wholesaler ID
        public int WholesalerId { get; set; }

        // Wholesaler 
        public Wholesaler Wholesaler { get; set; }
        
        // Product ID
        public int ProductId { get; set; }

        // Price
        public Decimal Price { get; set; }
    }
}