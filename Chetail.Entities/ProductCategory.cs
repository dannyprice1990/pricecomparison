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
    /// Represents a Product Category
    /// </summary>
    public class ProductCategory : EntityBase
    {
        // Category Name
        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        // List of Products
        public virtual ICollection<Product> Products { get; set; }
    }
}