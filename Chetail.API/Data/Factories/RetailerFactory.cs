using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.API.Data.Factories
{
    public class RetailerFactory
    {
        public Retailer CreateRetailer(DTO.Retailer retailer)
        {
            return new Retailer()
            {
                Id = retailer.Id,
                Created = retailer.Created,
                Modified = retailer.Modified,
                Address = retailer.Address,
                Name = retailer.Name
            };
        }

        public DTO.Retailer CreateRetailer(Retailer retailer)
        {
            return new DTO.Retailer()
            {
                Id = retailer.Id,
                Created = retailer.Created,
                Modified = retailer.Modified,
                Address = retailer.Address,
                Name = retailer.Name
            };
        }
    }
}