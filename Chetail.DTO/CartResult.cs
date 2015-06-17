using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.DTO
{
    public class CartResult:DTOBase
    {
        // List of results
        public ICollection<CartResultItem> CartResultItems { get; set; }

        // Top Result
        public CartResultItem TopResult { get; set; }
    }
}
