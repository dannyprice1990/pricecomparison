using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Entities
{
    /// <summary>
    /// Represents a Product
    /// </summary>
    public class Product : EntityBase
    {
        // Product Code
        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string Code { get; set; }

        // Product Description
        [Required(ErrorMessage = "*")]
        [StringLength(100)]     
        public string Desc { get; set; }

        // Product Category ID
        public int ProductCategoryId { get; set; }

        //Product Category
        public virtual ProductCategory ProductCategory { get; set; }

        // List of Product Prices
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }


    }
}
