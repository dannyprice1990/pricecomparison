using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Entities
{
    /// <summary>
    /// Represents a Product Price 
    /// -- Wholesaler, Product, Price
    /// </summary>
    public class ProductPrice:EntityBase
    {
        // Wholesaler ID
        public int WholesalerId { get; set; }

        // Wholesaler 
        public virtual Wholesaler Wholesaler { get; set; }

        // Product ID
        public int ProductId { get; set; }

        // Price
        public Decimal Price { get; set; }
    }
}
