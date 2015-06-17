using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class Product:DTOBase
    {
        // Product Code
        public string Code { get; set; }

        // Product Description
        public string Desc { get; set; }

        // Image URL
        public string ImageURL { get; set; }

        // Qty
        public int Qty { get; set; }

        // Product Category ID
        public int ProductCategoryId { get; set; }

        // List of Product Prices
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
