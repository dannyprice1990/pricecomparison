using Chetail.Entities;
using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Repository
{
    public interface IWholesalerRepository
    {
        /// Gets Wholesalers as IQueryable
        IQueryable<Wholesaler> GetWholesalers();
        /// Gets Wholesaler by ID
        Wholesaler GetWholesaler(int id);
        /// Adds a Wholesaler
        RepositoryActionResult<Wholesaler> AddWholesaler(Wholesaler newWholesaler);
        /// Updates a Wholesaler
        RepositoryActionResult<Wholesaler> UpdateWholesaler(Wholesaler updatedWholesaler);
        ///  Deletes a Wholesaler
        RepositoryActionResult<Wholesaler> DeleteWholesaler(int id);
    }
}
