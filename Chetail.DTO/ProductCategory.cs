using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class ProductCategory:DTOBase
    {
        // Category Name
        public string Name { get; set; }

        //Product Count
        public int ProductCount { get; set; }

        // List of Products
        public virtual ICollection<Product> Products { get; set; }
    }
}
