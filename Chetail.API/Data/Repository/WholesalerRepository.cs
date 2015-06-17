using Chetail.API.Data;
using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chetail.Repository
{
    public class WholesalerRepository:IWholesalerRepository
    {
        AppDBContext _ctx;
        public WholesalerRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Entities.Wholesaler> GetWholesalers()
        {
            return _ctx.Wholesalers;
        }
        public Wholesaler GetWholesaler(int id)
        {
            return _ctx.Wholesalers.Find(id);
        }
        public RepositoryActionResult<Wholesaler> AddWholesaler(Wholesaler newWholesaler)
        {
            try
            {
                //Adds new Wholesaler
                _ctx.Wholesalers.Add(newWholesaler);
                //Saves Changes
                var result = _ctx.SaveChanges();

                //Returns Wholesaler & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<Wholesaler>(newWholesaler, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Wholesaler>(newWholesaler, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Wholesaler>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Wholesaler> UpdateWholesaler(Wholesaler updatedWholesaler)
        {
            try
            {
                // Only update when Wholesaler already exists
                var existingWholesaler = _ctx.Wholesalers.FirstOrDefault(b => b.Id == updatedWholesaler.Id);
                if (existingWholesaler == null)
                {
                    return new RepositoryActionResult<Wholesaler>(updatedWholesaler, RepositoryActionStatus.NotFound);
                }

                // Change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet
                // set original entity state to detached
                _ctx.Entry(existingWholesaler).State = EntityState.Detached;

                // attach & save
                _ctx.Wholesalers.Attach(updatedWholesaler);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(updatedWholesaler).State = EntityState.Modified;

                // Save Changes
                var result = _ctx.SaveChanges();

                //Returns Book & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<Wholesaler>(updatedWholesaler, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Wholesaler>(updatedWholesaler, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Wholesaler>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Wholesaler> DeleteWholesaler(int id)
        {
            try
            {
                //Finds existing based on ID
                var existing = _ctx.Wholesalers.Where(b => b.Id == id).FirstOrDefault();
                if (existing != null)
                {
                    _ctx.Wholesalers.Remove(existing);
                    _ctx.SaveChanges();
                    //All went okay
                    return new RepositoryActionResult<Wholesaler>(null, RepositoryActionStatus.Deleted);
                }
                //Entity was not found
                return new RepositoryActionResult<Wholesaler>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Wholesaler>(null, RepositoryActionStatus.Error, ex);
            }
        }

    }
}