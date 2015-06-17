using Chetail.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Repository
{
    public interface ICartRepository
    {
        /// Gets Cart Results
        RepositoryActionResult<DTO.CartResult> GetCartResult(Cart cart);
    }
}