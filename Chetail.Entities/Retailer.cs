using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Entities
{
    /// <summary>
    /// Represents a Retailer
    /// </summary>
    public class Retailer:EntityBase
    {
        // Name
        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Name { get; set; }

        // Address
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
    }
}
