using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.API.Data.Factories
{
    public class WholesalerFactory
    {
        public Wholesaler CreateWholesaler(DTO.Wholesaler wholesaler)
        {
            return new Wholesaler()
            {
                Id = wholesaler.Id,
                Created = wholesaler.Created,
                Modified = wholesaler.Modified,
                Address=wholesaler.Address,
                Name = wholesaler.Name           
            };
        }

        public DTO.Wholesaler CreateWholesaler(Wholesaler wholesaler)
        {
            return new DTO.Wholesaler()
            {
                Id = wholesaler.Id,
                Created = wholesaler.Created,
                Modified = wholesaler.Modified,
                Address = wholesaler.Address,
                Name = wholesaler.Name    
            };
        }
    }
}